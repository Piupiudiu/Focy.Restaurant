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
});
