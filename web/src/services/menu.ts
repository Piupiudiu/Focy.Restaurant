import request from '@/utils/request';

export interface PagedResultDto<T> {
  items: T[];
  totalCount: number;
}

export interface MenuItemDto {
  id: string;
  name: string;
  description: string | null;
  imgUri: string | null;
  uri: string | null;
  isAvailable: string | null;
}

// 获取菜单列表
export async function getMenuList(name: string | null = null, skipCount: number = 0, maxResultCount: number = 10) {
  return request<PagedResultDto<MenuItemDto>>(`/menu/get?skipCount=${skipCount}&maxResultCount=${maxResultCount}` + (name == null ? "" : `&name=${name}`));
}