import pandas as pd
from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.by import By
from webdriver_manager.chrome import ChromeDriverManager
from .base_scraper import FPTShopScraper

class FPTShopBaseScraper(FPTShopScraper):
    """Base implementation for FPT Shop scrapers"""

    def __init__(self, headless=False, base_url=None):
        # Thiết lập Chrome options
        chrome_options = Options()
        if headless:
            chrome_options.add_argument("--headless")  # Chạy ẩn danh
        # Tắt thông báo trình duyệt
        chrome_options.add_argument("--disable-notifications")
        # Chặn popup không mong muốn
        chrome_options.add_argument("--disable-popup-blocking")
        chrome_options.add_argument("--no-sandbox")
        chrome_options.add_argument("--start-maximized")
        chrome_options.add_argument("--disable-dev-shm-usage")

        # Khởi tạo WebDriver
        self.driver = webdriver.Chrome(
            service=Service(ChromeDriverManager().install()),
            options=chrome_options
        )
        self.base_url = base_url

    def get_safe_text(self, element, selector, method=By.CSS_SELECTOR):
        """Trích xuất text từ một phần tử một cách an toàn"""
        try:
            return element.find_element(method, selector).text.strip()
        except:
            return ""

    def get_safe_attribute(self, element, selector, attribute, method=By.CSS_SELECTOR):
        """Trích xuất thuộc tính từ một phần tử một cách an toàn"""
        try:
            return element.find_element(method, selector).get_attribute(attribute)
        except:
            return ""

    def save_to_csv(self, products, filename):
        """Lưu dữ liệu sản phẩm vào file CSV"""
        import os

        if not products:
            print("Không có dữ liệu để lưu")
            return

        # Đường dẫn đến thư mục landing-zones/crawler
        landing_zone_path = os.path.join("landing-zones")
        
        # Tạo thư mục nếu chưa tồn tại
        os.makedirs(landing_zone_path, exist_ok=True)
        
        # Đường dẫn đầy đủ cho file
        filepath = os.path.join(landing_zone_path, filename)

        df = pd.DataFrame(products)
        df.to_csv(filepath, index=False, encoding='utf-8-sig')
        print(f"Đã lưu dữ liệu vào file {filepath}")

    def close(self):
        """Đóng WebDriver"""
        if self.driver:
            self.driver.quit()