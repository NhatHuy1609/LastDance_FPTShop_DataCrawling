import asyncio
from config import LANDING_ZONES_PATH, DATABASE_API_URL, BATCH_SIZE
from ingestion.laptop_ingestion import LaptopIngestion
from ingestion.gaming_gears_ingestion import GamingGearsIngestion
from ingestion.monitor_ingestion import MonitorIngestion

async def run_ingestion(ingestion):
    """Chạy một tiến trình nạp dữ liệu bất đồng bộ"""
    return await ingestion.ingest_async()

async def run_all_ingestions():
    """Chạy tất cả các tiến trình nạp dữ liệu cùng lúc"""
    # Danh sách các đối tượng nạp dữ liệu
    ingestion_tasks = [
        LaptopIngestion(
            api_url=DATABASE_API_URL,
            landing_zone_path=LANDING_ZONES_PATH,
            batch_size=BATCH_SIZE
        ),
        # GamingGearsIngestion(
        #     api_url=DATABASE_API_URL,
        #     landing_zone_path=LANDING_ZONES_PATH,
        #     batch_size=BATCH_SIZE
        # ),
#         MonitorIngestion(
#             api_url=DATABASE_API_URL,
#             landing_zone_path=LANDING_ZONES_PATH,
#             batch_size=BATCH_SIZE
#         ),
        # GamingGearsIngestion(
        #     api_url=DATABASE_API_URL,
        #     landing_zone_path=LANDING_ZONES_PATH,
        #     batch_size=BATCH_SIZE
        # )
        # Thêm các đối tượng nạp dữ liệu khác ở đây
        # PhoneIngestion(...),
        # TabletIngestion(...),
        # AccessoryIngestion(...),
    ]
    
    print(f"Bắt đầu nạp dữ liệu cho {len(ingestion_tasks)} loại dữ liệu")
    
    # Chạy tất cả các tiến trình nạp dữ liệu cùng lúc
    tasks = [run_ingestion(ingestion) for ingestion in ingestion_tasks]
    results = await asyncio.gather(*tasks)
    
    print("Hoàn thành tất cả quá trình nạp dữ liệu")
    return results

if __name__ == "__main__":
    """Hàm chính để chạy quá trình nạp dữ liệu."""
    # Chạy tất cả các tiến trình nạp dữ liệu cùng lúc
    asyncio.run(run_all_ingestions())
