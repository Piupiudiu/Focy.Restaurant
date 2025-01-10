import { getOrderList, OrderDto } from "@/services/order";
import { Card, Divider, DotLoading, InfiniteScroll, List } from "antd-mobile";
import { FC, PropsWithChildren, useState } from "react";
import noImageJpg from '../assets/noImage.png';

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

const ListItemContent: FC<
  PropsWithChildren<{
    item: OrderDto;
  }>
> = prop => {
  return (
    <Card title={prop.item.name} headerStyle={{ border: 'none' }}>
      <List>
        {
          prop.item.menus.map(menu => (
            <List.Item key={menu.id}>
              <div style={{ display: 'flex', flexDirection: 'row' }}>
                <img src={menu.imgUri ? `https://localhost:44366/${menu.imgUri}` : noImageJpg} style={{ width: "25%", aspectRatio: '1/1' }} />
                <div style={{ width: '75%', paddingLeft: '10px' }}>
                  <div>{menu.name}</div>
                  <div>数量: 1</div>
                </div>
              </div>
            </List.Item>
          ))
        }
      </List>
    </Card>
  )
}

export default function OrderPage() {
  const [orderList, setOrderList] = useState<OrderDto[]>([]);
  const [hasMore, setHasMore] = useState(true);
  const [nextSkipCount, setNextSkipCount] = useState(0);

  async function loadMore() {
    const append = await getOrderList(nextSkipCount);
    setOrderList(val => [...val, ...append.items]);
    setHasMore(append.items.length > 0);
    setNextSkipCount(nextSkipCount + 10);
  }

  return (
    <>
      <List header='订单'>
        {
          orderList.map(item => (
            <List.Item key={item.id}>
              <ListItemContent item={item} />
            </List.Item>
          ))
        }
      </List>
      <InfiniteScroll loadMore={loadMore} hasMore={hasMore}>
        <InfiniteScrollContent hasMore={hasMore} />
      </InfiniteScroll>
    </>
  );
}