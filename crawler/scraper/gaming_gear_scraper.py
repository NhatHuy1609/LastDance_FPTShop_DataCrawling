import time
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import NoSuchElementException
from .base.fptshop_base import FPTShopBaseScraper


class FPTShopGamingGearScraper(FPTShopBaseScraper):
    def __init__(self, headless=False):
        super().__init__(headless, base_url="https://fptshop.com.vn/phu-kien/gaming-gear")

    def load_all_products(self):
        """Nhấn nút 'Xem thêm' để load tất cả sản phẩm"""
        print("Đang tải tất cả sản phẩm gaming gear...")

        try:
            # Đếm số lượng sản phẩm ban đầu
            products = self.driver.find_elements(
                By.XPATH, '//*[@id="scroll-top"]/div[2]/div[3]/div[2]/div')
            prev_count = len(products)
            print(f"Số sản phẩm ban đầu: {prev_count}")

            click_count = 0
            max_attempts = 30  # Giới hạn số lần nhấn nút để tránh lặp vô hạn

            while click_count < max_attempts:
                try:
                    # Tìm nút "Xem thêm" với selector đầy đủ
                    load_more_button = self.driver.find_element(
                        By.CSS_SELECTOR,
                        "button.Button_root__LQsbl.Button_btnSmall__aXxTy.Button_whitePrimary__nkoMI.Button_btnIconRight__4VSUO.border.border-iconDividerOnWhite.px-4.py-2"
                    )

                    # Kiểm tra xem nút có hiển thị không
                    if load_more_button.is_displayed():
                        print(f"Nhấn nút 'Xem thêm' lần {click_count + 1}...")

                        # Cuộn đến nút "Xem thêm"
                        self.driver.execute_script(
                            "arguments[0].scrollIntoView({block: 'center'});", load_more_button)
                        time.sleep(1)

                        # Nhấn nút
                        load_more_button.click()

                        # Đợi để trang tải thêm sản phẩm
                        time.sleep(3)

                        # Kiểm tra xem có sản phẩm mới được thêm vào không
                        products = self.driver.find_elements(
                            By.XPATH, '//*[@id="scroll-top"]/div[2]/div[3]/div[2]/div')
                        current_count = len(products)

                        if current_count > prev_count:
                            print(
                                f"Đã tải thêm {current_count - prev_count} sản phẩm. Tổng: {current_count}")
                            prev_count = current_count
                            click_count += 1
                        else:
                            print(
                                "Không tìm thấy sản phẩm mới. Có thể đã tải tất cả.")
                            break
                    else:
                        print(
                            "Nút 'Xem thêm' không còn hiển thị. Đã tải tất cả sản phẩm.")
                        break

                except NoSuchElementException:
                    print("Không tìm thấy nút 'Xem thêm'. Đã tải tất cả sản phẩm.")
                    break
                except Exception as e:
                    print(f"Lỗi khi tải thêm sản phẩm: {e}")
                    break

            # Đợi thêm một chút để đảm bảo tất cả sản phẩm đã được render
            time.sleep(2)

            # Đếm lại số lượng sản phẩm cuối cùng
            products = self.driver.find_elements(
                By.XPATH, '//*[@id="scroll-top"]/div[2]/div[3]/div[2]/div')
            print(f"Tổng số sản phẩm đã tải: {len(products)}")

        except Exception as e:
            print(f"Lỗi trong quá trình tải tất cả sản phẩm: {e}")

    def scrape_products(self):
        """Implements the abstract method from the interface"""
        return self.scrape_gaming_gear()

    def scrape_gaming_gear(self):
        """Scrape dữ liệu gaming gear từ FPT Shop"""
        all_gaming_gear = []

        try:
            # Truy cập trang web
            print(f"Đang truy cập {self.base_url}")
            self.driver.get(self.base_url)

            # Đợi cho trang web load xong
            WebDriverWait(self.driver, 10).until(
                EC.presence_of_element_located(
                    (By.XPATH, '//*[@id="scroll-top"]/div[2]/div[3]/div[2]'))
            )

            # Load tất cả sản phẩm
            self.load_all_products()

            # Lấy container chính chứa các sản phẩm
            product_container = self.driver.find_element(
                By.XPATH, '//*[@id="scroll-top"]/div[2]/div[3]/div[2]')

            # Lấy tất cả sản phẩm trong container - sử dụng tất cả các div con trực tiếp
            products = product_container.find_elements(By.XPATH, './div')
            print(f"Tìm thấy {len(products)} sản phẩm để xử lý")

            # In các class của vài sản phẩm đầu tiên để debug
            for i in range(min(3, len(products))):
                try:
                    print(
                        f"Sản phẩm #{i+1} class: {products[i].get_attribute('class')}")
                except:
                    pass

            # Duyệt qua từng sản phẩm và lấy thông tin
            for i, product in enumerate(products):
                try:
                    # Kiểm tra xem div có phải là sản phẩm không
                    product_classes = product.get_attribute('class')
                    if not product_classes or not ('ProductCard_brandCard__VQQT8' in product_classes or 'ProductCard_cardDefault__km9c5' in product_classes):
                        continue

                    # Lấy thông tin sản phẩm
                    gear_data = {}

                    # Lấy tên sản phẩm
                    try:
                        gear_data["Tên sản phẩm"] = product.find_element(
                            By.CSS_SELECTOR, "h3.ProductCard_cardTitle__HlwIo a"
                        ).text.strip()
                    except:
                        gear_data["Tên sản phẩm"] = "N/A"

                    # Lấy URL sản phẩm
                    try:
                        gear_data["URL"] = product.find_element(
                            By.CSS_SELECTOR, "h3.ProductCard_cardTitle__HlwIo a"
                        ).get_attribute("href")
                    except:
                        gear_data["URL"] = "N/A"

                    # Lấy URL hình ảnh
                    try:
                        gear_data["Hình ảnh"] = product.find_element(
                            By.CSS_SELECTOR, "img"
                        ).get_attribute("src")
                    except:
                        gear_data["Hình ảnh"] = "N/A"
                    
                    # Kiểm tra xem có phải "Hàng sắp về" không
                    try:
                        coming_soon_text = product.find_element(
                            By.CSS_SELECTOR, "p.ProductCard_displayPriceText__nfghi"
                        ).text.strip()
                        
                        if "Hàng sắp về" in coming_soon_text:
                            gear_data["Trạng thái"] = "Hàng sắp về"
                            gear_data["Giá gốc"] = "N/A"
                            gear_data["Giảm giá %"] = "N/A"
                            gear_data["Giá hiện tại"] = "N/A"
                            gear_data["Giảm"] = "N/A"
                            # Bỏ qua phần lấy giá ở dưới
                        else:
                            gear_data["Trạng thái"] = "Có hàng"
                            # Tiếp tục lấy thông tin giá như bình thường
                    except NoSuchElementException:
                        gear_data["Trạng thái"] = "Có hàng"
                        # Tiếp tục lấy thông tin giá như bình thường
                    except Exception as e:
                        gear_data["Trạng thái"] = "Có hàng"
                        print(f"Lỗi khi kiểm tra hàng sắp về: {e}")
                    
                    # Lấy thông tin giá nếu không phải hàng sắp về
                    if gear_data.get("Trạng thái") != "Hàng sắp về":
                        price_wrapper = None
                        try:
                            price_wrapper = product.find_element(
                                By.CSS_SELECTOR,
                                "div.Price_priceWrap__Ovh8a.Price_priceDefault__LB7d8"
                            )
                        except:
                            pass

                        if price_wrapper:
                            # Giá gốc
                            try:
                                original_price = price_wrapper.find_element(
                                    By.CSS_SELECTOR, "p span:first-child"
                                ).text.strip()
                                gear_data["Giá gốc"] = original_price
                            except:
                                gear_data["Giá gốc"] = "N/A"

                            # Tỷ lệ giảm giá
                            try:
                                discount_percent = price_wrapper.find_element(
                                    By.CSS_SELECTOR, "p span:nth-child(2)"
                                ).text.strip()
                                gear_data["Giảm giá %"] = discount_percent
                            except:
                                gear_data["Giảm giá %"] = "N/A"

                            # Giá hiện tại
                            try:
                                current_price = price_wrapper.find_element(
                                    By.CSS_SELECTOR, "p.Price_currentPrice__PBYcv"
                                ).text.strip()
                                gear_data["Giá hiện tại"] = current_price
                            except:
                                gear_data["Giá hiện tại"] = "N/A"

                            # Số tiền giảm
                            try:
                                discount_amount = price_wrapper.find_element(
                                    By.CSS_SELECTOR, "p.text-textOnSemanticGreenDefault.f1-regular"
                                ).text.strip()
                                gear_data["Giảm"] = discount_amount
                            except:
                                gear_data["Giảm"] = "N/A"
                        else:
                            gear_data["Giá gốc"] = "N/A"
                            gear_data["Giảm giá %"] = "N/A"
                            gear_data["Giá hiện tại"] = "N/A"
                            gear_data["Giảm"] = "N/A"

                    # Lấy loại gear (chuột, bàn phím, tai nghe, v.v.)
                    try:
                        # Thử xác định loại gear từ tên sản phẩm
                        product_name = gear_data["Tên sản phẩm"].lower()
                        if "chuột" in product_name or "mouse" in product_name:
                            gear_data["Loại"] = "Chuột gaming"
                        elif "bàn phím" in product_name or "keyboard" in product_name:
                            gear_data["Loại"] = "Bàn phím gaming"
                        elif "tai nghe" in product_name or "headphone" in product_name or "headset" in product_name:
                            gear_data["Loại"] = "Tai nghe gaming"
                        elif "ghế" in product_name or "chair" in product_name:
                            gear_data["Loại"] = "Ghế gaming"
                        elif "miếng lót" in product_name or "pad" in product_name:
                            gear_data["Loại"] = "Bàn di chuột"
                        else:
                            gear_data["Loại"] = "Phụ kiện gaming khác"
                    except:
                        gear_data["Loại"] = "Phụ kiện gaming khác"

                    # Chỉ thêm sản phẩm vào danh sách nếu có tên sản phẩm hợp lệ
                    if gear_data["Tên sản phẩm"] != "N/A":
                        all_gaming_gear.append(gear_data)

                    # In thông tin tiến trình
                    if (i + 1) % 10 == 0:
                        print(
                            f"Đã xử lý {i + 1}/{len(products)} sản phẩm, thu thập được {len(all_gaming_gear)} sản phẩm")

                except Exception as e:
                    print(f"Lỗi khi xử lý sản phẩm {i + 1}: {e}")

            print(f"Đã thu thập thông tin của {len(all_gaming_gear)} sản phẩm")
            return all_gaming_gear

        except Exception as e:
            print(f"Lỗi khi scraping: {e}")
            return all_gaming_gear 