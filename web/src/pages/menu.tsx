import { getMenuList, MenuItemDto } from "@/services/menu";
import { Divider, DotLoading, Grid, InfiniteScroll, SearchBar, Toast } from "antd-mobile";
import { AddCircleOutline } from "antd-mobile-icons";
import { useState } from "react";
import '@/layouts/menu.less';
import noImageJpg from '../assets/noImage.png';
import { createShoppingCart } from "@/services/shoppingCart";

const InfiniteScrollContent = ({ hasMore }: { hasMore?: boolean }) => {
  return (
    <>
      {hasMore ? (
        <>
          <span>Loading</span>
          <DotLoading />
        </>
      ) : (
        <Divider style={{ width: '100%', borderStyle: 'ridge' }}>我是有底线的</Divider>
      )}
    </>
  )
}

export default function MenuPage() {
  const [menuList, setMenuList] = useState<MenuItemDto[]>([]);
  const [hasMore, setHasMore] = useState(true);
  const [nextSkipCount, setNextSkipCount] = useState(0);
  const [searchName, setSearchName] = useState<string | null>(null);

  async function loadMore() {
    const append = await getMenuList(searchName, nextSkipCount);
    setMenuList(val => [...val, ...append.items]);
    setHasMore(append.items.length > 0);
    setNextSkipCount(nextSkipCount + 10);
  }

  const doSearch = (name: string | null = null) => {
    if (name !== searchName) {
      setNextSkipCount(0);
    }
    setSearchName(name);
    setMenuList([]);
    setHasMore(true);
    loadMore();
  }

  const onAddBtnClick = (menuId: string) => {
    createShoppingCart(menuId).then(res => {
      if (res) {
        Toast.show({
          icon: 'success',
          content: '添加成功!',
        })
      }
      else {
        Toast.show({
          icon: 'fail',
          content: '添加失败!',
        })
      }
    });
  }

  return (
    <>
      <div style={{ backgroundColor: 'white', position: 'sticky', top: '0px' }}>
        <SearchBar onSearch={value => { doSearch(value) }} onClear={() => { doSearch() }} style={{ paddingBottom: '4px' }} placeholder='请输入内容' />
      </div>
      <div>
        <Grid columns={2} gap={8}>
          {
            menuList.map(item => (
              <Grid.Item key={item.id}>
                <div className="menu-item">
                  <img src={item.imgUri ? `https://localhost:44366/${item.imgUri}` : noImageJpg} alt="" className="menu-item-image" />
                  <div className="menu-item-content">
                    <span>{item.name}</span>
                    <AddCircleOutline className="menu-item-add" onClick={() => onAddBtnClick(item.id)} />
                  </div>
                </div>
              </Grid.Item>
            ))
          }
        </Grid>
        <InfiniteScroll loadMore={loadMore} hasMore={hasMore}>
          <InfiniteScrollContent hasMore={hasMore} />
        </InfiniteScroll>
      </div>
    </>
  );
}