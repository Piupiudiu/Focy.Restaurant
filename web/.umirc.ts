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
      target: 'https://localhost:44366/api',
      changeOrigin: true,
      secure: false, // 忽略 SSL 自签名证书错误
      pathRewrite: { '^/api': '' },
    },
  },
});
