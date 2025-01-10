import request from '@/utils/request';
import { MenuItemDto } from './menu';
import { PagedResultDto } from './commonType';

export interface OrderCreateDto {
    name: string;
    remark: string | null;
	menuIds: string[];
}

export interface OrderDto {
    id: string;
    name: string;
    remark: string | null;
    status: number;
	menus: MenuItemDto[];
}

//创建订单
export async function createOrder(data: OrderCreateDto) {
	return request<boolean>(`/order/create`, {
		method: 'POST',
		data,
	});
}

//查询订单
export async function getOrderList(skipCount: number = 0, maxResultCount: number = 10) {
	return request<PagedResultDto<OrderDto>>(`/order/get?skipCount=${skipCount}&maxResultCount=${maxResultCount}`);
}