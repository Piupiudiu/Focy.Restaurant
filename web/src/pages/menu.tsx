import { getMenuList, MenuItemDto } from "@/services/menu";
import { Grid } from "antd-mobile";
import { useEffect, useState } from "react";

export default function MenuPage() {
  const [menuList, setMenuList] = useState<MenuItemDto[]>([]);

  useEffect(() => {
    getMenuList().then(res => {
      setMenuList(res.items);
    })
  }, []);

  return (
    <>
      <Grid columns={2} gap={8}>
        {
          menuList.map(item => (
            <Grid.Item key={item.id}>
              <div>
                <img src={`https://localhost:44366/${item.imgUri ?? ''}`} alt="" style={{width: '100%', height: 'auto', objectFit: 'contain'}} />
                <div>{item.name}</div>
              </div>
            </Grid.Item>
          ))
        }
      </Grid>
    </>
  );
}