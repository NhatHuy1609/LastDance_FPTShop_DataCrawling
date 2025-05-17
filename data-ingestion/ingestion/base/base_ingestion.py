import os
import pandas as pd
import aiohttp
import asyncio
from abc import ABC, abstractmethod
from typing import List, Dict, Any

class BaseIngestion(ABC):
    """Lớp cơ sở cho quá trình nạp dữ liệu."""
    
    def __init__(self, api_url: str, landing_zone_path: str, batch_size: int = 50):
        """
        Khởi tạo lớp nạp dữ liệu cơ sở.
        
        Args:
            api_url: URL cơ sở cho API
            landing_zone_path: Đường dẫn đến thư mục landing zone
            batch_size: Số lượng bản ghi gửi trong mỗi lô
        """
        self.api_url = api_url
        self.landing_zone_path = landing_zone_path
        self.batch_size = batch_size
    
    def read_csv_data(self, filename: str) -> pd.DataFrame:
        """
        Đọc dữ liệu từ file CSV trong landing zone.
        
        Args:
            filename: Tên file CSV
            
        Returns:
            DataFrame chứa dữ liệu CSV
        """
        file_path = os.path.join(self.landing_zone_path, filename)
        print(f"Đang đọc file {file_path}")
        
        try:
            df = pd.read_csv(file_path)
            print(f"Đã đọc {len(df)} bản ghi từ {filename}")
            return df
        except Exception as e:
            print(f"Lỗi khi đọc {filename}: {str(e)}")
            raise
    
    async def send_item_to_api_async(self, session: aiohttp.ClientSession, endpoint: str, item: Dict[str, Any]) -> Dict[str, Any]:
        """
        Gửi một item đến API bất đồng bộ.
        
        Args:
            session: Phiên aiohttp
            endpoint: Endpoint API để gửi dữ liệu
            item: Dữ liệu cần gửi
            
        Returns:
            Phản hồi API
        """
        url = f"{self.api_url}/{endpoint}"

        try:
            async with session.post(url, json=item) as response:
                response.raise_for_status()
                return await response.json()
        except aiohttp.ClientError as e:
            print(f"Yêu cầu API bất đồng bộ thất bại: {str(e)}")
            return None
    
    async def send_batch_to_api_async(self, endpoint: str, data_batch: List[Dict[str, Any]]) -> List[Dict[str, Any]]:
        """
        Gửi một lô dữ liệu đến API bất đồng bộ.
        
        Args:
            endpoint: Endpoint API để gửi dữ liệu
            data_batch: Danh sách các từ điển chứa dữ liệu
            
        Returns:
            Danh sách các phản hồi API
        """
        async with aiohttp.ClientSession() as session:
            tasks = [self.send_item_to_api_async(session, endpoint, item) for item in data_batch]
            results = await asyncio.gather(*tasks)
            # Lọc ra các kết quả None
            return [result for result in results if result is not None]
    
    @abstractmethod
    def transform_data(self, df: pd.DataFrame) -> List[Dict[str, Any]]:
        """
        Chuyển đổi dữ liệu từ DataFrame sang định dạng API.
        
        Args:
            df: DataFrame chứa dữ liệu nguồn
            
        Returns:
            Danh sách các từ điển theo định dạng API mong đợi
        """
        pass
    
    @abstractmethod
    async def ingest_async(self) -> None:
        """Thực thi quá trình nạp dữ liệu bất đồng bộ."""
        pass 