import { extend } from 'umi-request';

// 创建 umi-request 实例
const request = extend({
  prefix: '/api', // 接口前缀
  timeout: 10000, // 请求超时时间
  headers: {
    'Content-Type': 'application/json',
  },
});

// 请求拦截器
request.interceptors.request.use((url, options) => {
  const token = localStorage.getItem('ROCP_token')?.replaceAll('"', '') // 从本地存储中获取 token
  return {
    url,
    options: {
      ...options,
      headers: {
        ...options.headers,
        Authorization: `Bearer ${token}`, // 添加 token 到请求头
      },
    },
  };
});

// 响应拦截器
request.interceptors.response.use(async (response) => {
  const data = await response.clone().json();
  if (data.code !== 200) {
    console.error(data.message || '请求失败');
  }
  return response;
});

export default request;