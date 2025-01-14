import { deleteOrder, getOrderList, OrderDto, updateOrderStatus } from "@/services/order";
import { Card, Divider, DotLoading, InfiniteScroll, List, Radio, Selector, Space, SwipeAction, Tabs } from "antd-mobile";
import { FC, PropsWithChildren, useState } from "react";
import noImageJpg from '../assets/noImage.png';
import { RadioValue } from "antd-mobile/es/components/radio";
import res from "antd-mobile-icons/es/AaOutline";

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
    onRadioChange: (val: RadioValue) => void;
    deleteAction: () => void;
  }>
> = prop => {
  return (
    <Card title={prop.item.name} headerStyle={{ border: 'none' }}>
      <SwipeAction
        key={prop.item.id}
        rightActions={[
          {
            key: 'delete',
            text: '删除',
            color: 'danger',
            onClick: () => {
              prop.deleteAction();
            }
          },
        ]}
      >
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
          <List.Item style={{ fontSize: 15 }}>备注:{prop.item.remark}</List.Item>
          <List.Item>
            <Radio.Group defaultValue={prop.item.status.toString()} onChange={prop.onRadioChange}>
              <Space direction='horizontal'>
                <Radio style={{
                  '--icon-size': '18px',
                  '--font-size': '14px',
                  '--gap': '6px',
                }} value='0'>已下单</Radio>
                <Radio style={{
                  '--icon-size': '18px',
                  '--font-size': '14px',
                  '--gap': '6px',
                }} value='1'>进行中</Radio>
                <Radio style={{
                  '--icon-size': '18px',
                  '--font-size': '14px',
                  '--gap': '6px',
                }} value='2'>已完成</Radio>
                <Radio style={{
                  '--icon-size': '18px',
                  '--font-size': '14px',
                  '--gap': '6px',
                }} value='3'>已取消</Radio>
              </Space>
            </Radio.Group>
          </List.Item>
        </List>
      </SwipeAction>
    </Card>
  )
}

export default function OrderPage() {
  const [orderList, setOrderList] = useState<OrderDto[]>([]);
  const [hasMore, setHasMore] = useState(true);
  const [nextSkipCount, setNextSkipCount] = useState(0);
  const [status, setStatus] = useState<null | number>(0);

  const tabItems = [
    { key: '0', title: '已下单' },
    { key: '1', title: '进行中' },
    { key: '2', title: '已完成' },
    { key: '3', title: '已取消' },
  ]

  async function loadMore() {
    const append = await getOrderList(nextSkipCount, 10, status);
    setOrderList(val => [...val, ...append.items]);
    setHasMore(append.items.length > 9);
    setNextSkipCount(nextSkipCount + 10);
  }

  const resetOrderList = (key: number | null) => {
    setOrderList([]);
    getOrderList(0, 10, key).then(res => {
      if (res.items.length > 0) {
        setOrderList(res.items);
        setHasMore(res.items.length > 9)
        setNextSkipCount(10);
      }
    });
  }
  const onTabChange = (key: string) => {
    setStatus(Number(key));
    resetOrderList(Number(key));
  }

  const onRadioChange = (val: RadioValue, item: OrderDto) => {
    updateOrderStatus(item.id, Number(val)).then(res => {
      if (res) {
        resetOrderList(status);
      }
    });
  }

  const deleteAction = (id: string) => {
    deleteOrder(id).then(res => {
      if (res) {
        resetOrderList(status);
      }
    });
  }

  return (
    <>
      <Tabs onChange={onTabChange}>
        {
          tabItems.map(item => (
            <Tabs.Tab title={item.title} key={item.key}>
              <List>
                {
                  orderList.map(item => (
                    <List.Item key={item.id}>
                      <ListItemContent item={item} onRadioChange={val => onRadioChange(val, item)} deleteAction={() => deleteAction(item.id)} />
                    </List.Item>
                  ))
                }
              </List>
              <InfiniteScroll loadMore={loadMore} hasMore={hasMore}>
                <InfiniteScrollContent hasMore={hasMore} />
              </InfiniteScroll>
            </Tabs.Tab>
          ))
        }
      </Tabs>
    </>
  );
}