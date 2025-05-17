import pandas as pd
import re
import asyncio
from typing import List, Dict, Any
from .base.base_ingestion import BaseIngestion

class LaptopIngestion(BaseIngestion):
    """Lớp nạp dữ liệu laptop từ FPT Shop."""
    
    def __init__(self, api_url: str, landing_zone_path: str, batch_size: int = 50):
        """
        Khởi tạo lớp nạp dữ liệu laptop.
        
        Args:
            api_url: URL cơ sở cho API
            landing_zone_path: Đường dẫn đến thư mục landing zone
            batch_size: Số lượng bản ghi gửi trong mỗi lô
        """
        super().__init__(api_url, landing_zone_path, batch_size)
        self.csv_filename = "fpt_laptops.csv"
        self.api_endpoint = "Laptop"
    
    def _parse_price(self, price_str: str) -> float:
        """
        Phân tích chuỗi giá thành số thực.
        
        Args:
            price_str: Chuỗi giá (ví dụ: "15.590.000 ₫")
            
        Returns:
            Giá dưới dạng số thực
        """
        if not price_str or pd.isna(price_str):
            return 0.0
            
        # Loại bỏ ký hiệu tiền tệ và dấu chấm, sau đó chuyển đổi thành số thực
        price_str = re.sub(r'[^\d]', '', price_str)
        try:
            return float(price_str)
        except ValueError:
            print(f"Không thể phân tích giá: {price_str}")
            return 0.0
    
    def transform_data(self, df: pd.DataFrame) -> List[Dict[str, Any]]:
        """
        Chuyển đổi dữ liệu laptop từ DataFrame sang định dạng API.
        
        Args:
            df: DataFrame chứa dữ liệu laptop
            
        Returns:
            Danh sách các từ điển theo định dạng API mong đợi
        """
        transformed_data = []
        
        for _, row in df.iterrows():
            # Phân tích giá gốc và giá khuyến mãi
            original_price = self._parse_price(row['Giá gốc'])
            current_price = self._parse_price(row['Giá hiện tại'])
            
            # Tạo đối tượng laptop
            laptop = {
                "name": row['Tên sản phẩm'],
                "url": row['URL'],
                "imageUrl": row['Hình ảnh'],
                "price": original_price,
                "priceDiscount": current_price
            }
            
            transformed_data.append(laptop)
        
        return transformed_data
    
    def read_and_clean_csv(self, filename: str) -> pd.DataFrame:
        """
        Đọc và làm sạch dữ liệu từ file CSV trong landing zone.
        
        Args:
            filename: Tên file CSV
            
        Returns:
            DataFrame đã được làm sạch
        """
        # Đọc dữ liệu từ file CSV
        df = self.read_csv_data(filename)
        
        # Kiểm tra dữ liệu trùng lặp trước khi xử lý
        total_records = len(df)
        
        # Loại bỏ các dòng trùng lặp dựa trên URL
        if 'URL' in df.columns:
            duplicate_count_before = df.duplicated(subset=['URL']).sum()
            if duplicate_count_before > 0:
                print(f"Phát hiện {duplicate_count_before} bản ghi trùng lặp dựa trên URL")
                df = df.drop_duplicates(subset=['URL'], keep='first')
                print(f"Đã loại bỏ các bản ghi trùng lặp, còn lại {len(df)} bản ghi")
        
        # Loại bỏ các dòng trùng lặp hoàn toàn
        duplicate_count = df.duplicated().sum()
        if duplicate_count > 0:
            print(f"Phát hiện {duplicate_count} bản ghi hoàn toàn giống nhau")
            df = df.drop_duplicates(keep='first')
            print(f"Đã loại bỏ các bản ghi trùng lặp hoàn toàn, còn lại {len(df)} bản ghi")
        
        # Thông báo tổng số bản ghi đã loại bỏ
        removed_records = total_records - len(df)
        if removed_records > 0:
            print(f"Tổng cộng đã loại bỏ {removed_records} bản ghi trùng lặp")
        else:
            print("Không phát hiện bản ghi trùng lặp nào")
        
        return df
    
    async def ingest_async(self) -> None:
        """Thực thi quá trình nạp dữ liệu laptop bất đồng bộ."""
        # Đọc và làm sạch dữ liệu CSV
        df = self.read_and_clean_csv(self.csv_filename)
        
        # Chuyển đổi dữ liệu
        transformed_data = self.transform_data(df)
        
        # Gửi dữ liệu theo lô bất đồng bộ
        total_records = len(transformed_data)
        print(f"Đang gửi {total_records} bản ghi laptop đến API (bất đồng bộ)")
        
        batch_tasks = []
        for i in range(0, total_records, self.batch_size):
            batch = transformed_data[i:i + self.batch_size]
            batch_number = i//self.batch_size + 1
            total_batches = (total_records + self.batch_size - 1)//self.batch_size
            
            # Tạo task cho mỗi batch
            task = asyncio.create_task(self._process_batch_async(batch, batch_number, total_batches))
            batch_tasks.append(task)
        
        # Chờ tất cả các batch hoàn thành
        await asyncio.gather(*batch_tasks)
        
        print("Hoàn thành quá trình nạp dữ liệu laptop (bất đồng bộ)")
        
    async def _process_batch_async(self, batch: List[Dict[str, Any]], batch_number: int, total_batches: int) -> None:
        """
        Xử lý một lô dữ liệu bất đồng bộ.
        
        Args:
            batch: Lô dữ liệu cần xử lý
            batch_number: Số thứ tự của lô
            total_batches: Tổng số lô
        """
        results = await self.send_batch_to_api_async(self.api_endpoint, batch)
        print(f"Đã xử lý thành công {len(results)} bản ghi trong lô {batch_number}/{total_batches}")
        return results 