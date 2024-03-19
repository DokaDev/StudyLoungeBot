from bs4 import BeautifulSoup
import requests

def scrap_img():
    url = "https://www.bobful.com/bbs/board.php?bo_table=cook&wr_id=41&cook_id=24"
    url = "https://www.bobful.com/bbs/cook_list.php?bo_table=cook&cook_id=24"
    html = requests.get(url)
    soup = BeautifulSoup(html.content,"html.parser")
    img = soup.select_one("a.view_image > img").attrs["src"]
    return img