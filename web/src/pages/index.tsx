import { Link, Outlet } from 'umi';
import styles from '../layouts/index.less';
import { Modal, TabBar } from 'antd-mobile';
import { FC, useContext, useEffect, useState } from 'react';
import { FileOutline, HandPayCircleOutline, ShopbagOutline, UserOutline } from 'antd-mobile-icons';
import { history, useLocation } from 'umi';
import { AuthContext, AuthProvider, IAuthContext, TAuthConfig, TRefreshTokenExpiredEvent } from 'react-oauth2-code-pkce';

const authConfig: TAuthConfig = {
  clientId: 'Restaurant_Web',
  authorizationEndpoint: 'https://localhost:44382/connect/authorize',
  tokenEndpoint: 'https://localhost:44382/connect/token',
  redirectUri: 'http://localhost:8000',
  scope: 'Restaurant offline_access',
  onRefreshTokenExpire: (event: TRefreshTokenExpiredEvent) => event.logIn(undefined, undefined, "popup"),
}

const Bottom: FC = () => {
  const location = useLocation()

  const setRouteActive = (value: string) => {
    history.push(value)
  }
  const tabs = [
    {
      key: '/menu',
      title: '菜单',
      icon: <FileOutline />,
    },
    {
      key: '/shopping-cart',
      title: '购物车',
      icon: <ShopbagOutline />,
    },
    {
      key: '/order',
      title: '订单',
      icon: <HandPayCircleOutline />,
    },
    // {
    //   key: '/me',
    //   title: '我的',
    //   icon: <UserOutline />,
    // },
  ]

  return (
    <TabBar activeKey={location.pathname} onChange={value => setRouteActive(value)}>
      {tabs.map(item => (
        <TabBar.Item key={item.key} icon={item.icon} title={item.title} />
      ))}
    </TabBar>
  )
}
export default function HomePage() {
  const { token, tokenData } = useContext<IAuthContext>(AuthContext)
  const [visible, setVisible] = useState<boolean>(true);

  // useEffect(() => {
  //   history.push('/menu')
  // }, []);

  return (
    <AuthProvider authConfig={authConfig}>
      <div className={styles.app}>
        <div className={styles.top}>
          <h1>小菜馆</h1>
        </div>
        <div className={styles.body}>
          <Outlet />
        </div>
        <div className={styles.bottom}>
          <Bottom />
        </div>
      </div>
      <Modal visible={visible} content="👏欢迎来到凉皮Sir的小菜馆!!!👏" onClose={() => { history.push("/menu"); setVisible(false) }} closeOnMaskClick={true} />
    </AuthProvider>
  );
}
