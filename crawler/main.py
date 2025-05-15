from scraper.laptop_scraper import FPTShopLaptopScraper
from scraper.phone_scraper import FPTShopPhoneScraper
from scraper.monitor_scraper import FPTShopMonitorScraper
from scraper.washingmachine_scraper import FPTShopWashingMachineScraper

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

def run_monitor_scraper(headless=False):
    print("\n" + "="*50)
    print("STARTING MONITOR SCRAPING")
    print("="*50 + "\n")

    monitor_scraper = FPTShopMonitorScraper(headless=headless)

    try:
        # Scrape dữ liệu màn hình
        monitors = monitor_scraper.scrape_products()

        # In số lượng sản phẩm đã scrape
        print(f"Đã scrape {len(monitors)} màn hình")
        
        # Lưu dữ liệu vào file CSV
        monitor_scraper.save_to_csv(monitors, "fpt_monitors.csv")

    finally:
        # Đóng WebDriver
        monitor_scraper.close()

    return monitors



def run_washingmachine_scraper(headless=False):
    print("\n" + "="*50)
    print("STARTING WASHING MACHINE SCRAPING")
    print("="*50 + "\n")

    washingmachine_scraper = FPTShopWashingMachineScraper(headless=headless)

    try:
        # Scrape dữ liệu màn hình
        washingmachine = washingmachine_scraper.scrape_products()

        # In số lượng sản phẩm đã scrape
        print(f"Đã scrape {len(washingmachine)} máy giặt")
        
        # Lưu dữ liệu vào file CSV
        washingmachine_scraper.save_to_csv(washingmachine, "fpt_washingmachines.csv")

    finally:
        # Đóng WebDriver
        washingmachine_scraper.close()

    return washingmachine


if __name__ == "__main__":
    # Cài đặt headless=True để chạy ẩn danh không hiển thị trình duyệt
    headless_mode = False

    # Scrape laptops
    laptops = run_laptop_scraper(headless=headless_mode)

    # Scrape phones
    phones = run_phone_scraper(headless=headless_mode)

    # Scrape monitors
    monitors = run_monitor_scraper(headless=headless_mode)

    washingmachine = run_washingmachine_scraper(headless=headless_mode)
