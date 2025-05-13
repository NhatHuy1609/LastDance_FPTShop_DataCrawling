from scraper.laptop_scraper import FPTShopLaptopScraper
from scraper.phone_scraper import FPTShopPhoneScraper


def run_laptop_scraper(headless=False):
    print("\n" + "="*50)
    print("STARTING LAPTOP SCRAPING")
    print("="*50 + "\n")

    laptop_scraper = FPTShopLaptopScraper(headless=headless)

    try:
        # Scrape dữ liệu laptop
        laptops = laptop_scraper.scrape_products()

        # In số lượng sản phẩm đã scrape
        print(f"Đã scrape {len(laptops)} laptop")

        # Lưu dữ liệu vào file CSV
        laptop_scraper.save_to_csv(laptops, "fpt_laptops.csv")

    finally:
        # Đóng WebDriver
        laptop_scraper.close()

    return laptops


def run_phone_scraper(headless=False):
    print("\n" + "="*50)
    print("STARTING PHONE SCRAPING")
    print("="*50 + "\n")

    phone_scraper = FPTShopPhoneScraper(headless=headless)

    try:
        # Scrape dữ liệu điện thoại
        phones = phone_scraper.scrape_products()

        # In số lượng sản phẩm đã scrape
        print(f"Đã scrape {len(phones)} điện thoại")

        # Lưu dữ liệu vào file CSV
        phone_scraper.save_to_csv(phones, "fpt_phones.csv")

    finally:
        # Đóng WebDriver
        phone_scraper.close()

    return phones


if __name__ == "__main__":
    # Cài đặt headless=True để chạy ẩn danh không hiển thị trình duyệt
    headless_mode = False

    # Scrape laptops
    laptops = run_laptop_scraper(headless=headless_mode)

    # Scrape phones
    phones = run_phone_scraper(headless=headless_mode)
