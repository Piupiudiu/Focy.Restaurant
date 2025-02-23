import { defineConfig } from "umi";

export default defineConfig({
  routes: [
    {
      path: "/",
      component: "index",
      routes: [
        {
          path: "/menu",
          component: "menu",
        },
        {
          path: "/shopping-cart",
          component: "shoppingCart",
        },
        {
          path: "/order",
          component: "order",
        },
      ]
    },
  ],
  npmClient: 'pnpm',
  proxy: {
    '/api': {
      target: process.env.NODE_ENV == "development" ? process.env.UMI_APP_DEV_PROXY_API_URI : process.env.UMI_APP_PROD_PROXY_API_URI,
      changeOrigin: true,
      secure: false, // 忽略 SSL 自签名证书错误
      pathRewrite: { '^/api': '' },
    },
  },
});
