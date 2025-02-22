import request from '@/utils/request';
import { PagedResultDto } from './commonType';

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
  return request<PagedResultDto<MenuItemDto>>(`/menu/get?isFront=true&skipCount=${skipCount}&maxResultCount=${maxResultCount}` + (name == null ? "" : `&name=${name}`));
}