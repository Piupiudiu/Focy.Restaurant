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
export async function getOrderList(skipCount: number = 0, maxResultCount: number = 10, status: number | null = null) {
	return request<PagedResultDto<OrderDto>>(`/order/get?skipCount=${skipCount}&maxResultCount=${maxResultCount}${status == null ? "" : `&status=${status}`}`);
}

//修改订单状态
export async function updateOrderStatus(id: string, status: number) {
	return request<boolean>(`/order/update`, {
		method: 'PUT',
		data: {
			id,
			status
		}
	});
}

//删除订单
export async function deleteOrder(id: string) {
	return request<boolean>(`/order/delete?id=${id}`, {
		method: 'DELETE',
	});
}