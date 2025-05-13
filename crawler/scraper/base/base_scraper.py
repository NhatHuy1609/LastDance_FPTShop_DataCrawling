from abc import ABC, abstractmethod

class FPTShopScraper(ABC):
    """Interface for FPT Shop scrapers"""

    @abstractmethod
    def __init__(self, headless=False):
        pass

    @abstractmethod
    def load_all_products(self):
        """Nhấn nút 'Xem thêm' để load tất cả sản phẩm"""
        pass

    @abstractmethod
    def get_safe_text(self, element, selector, method=None):
        """Trích xuất text từ một phần tử một cách an toàn"""
        pass

    @abstractmethod
    def get_safe_attribute(self, element, selector, attribute, method=None):
        """Trích xuất thuộc tính từ một phần tử một cách an toàn"""
        pass

    @abstractmethod
    def scrape_products(self):
        """Scrape dữ liệu sản phẩm từ FPT Shop"""
        pass

    @abstractmethod
    def save_to_csv(self, products, filename):
        """Lưu dữ liệu sản phẩm vào file CSV"""
        pass

    @abstractmethod
    def close(self):
        """Đóng WebDriver"""
        pass
