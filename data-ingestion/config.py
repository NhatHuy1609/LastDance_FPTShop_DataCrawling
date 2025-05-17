import os
from dotenv import load_dotenv

# Tải biến môi trường từ file .env
load_dotenv()

# Cài đặt cấu hình
LANDING_ZONES_PATH = os.getenv('LANDING_ZONES_PATH', '../crawler/landing-zones')
DATABASE_API_URL = os.getenv('DATABASE_API_URL', 'http://localhost:5000')

# Kích thước lô cho các yêu cầu API
BATCH_SIZE = int(os.getenv('BATCH_SIZE', '50'))
