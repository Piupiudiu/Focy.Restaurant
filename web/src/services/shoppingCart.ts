import request from '@/utils/request';

export interface ShoopingCartItemDto {
	id: string;
	menuId: string;
	name: string;
	imgUri: string | null;
}

//添加商品到购物车
export async function createShoppingCart(menuId: string) {
	return request<boolean>(`/shopping-cart/create`, {
		method: 'PUT',
		data: { menuId },
	});
}

//获取购物车列表
export async function getShoppingCartList() {
	return request<ShoopingCartItemDto[]>(`/shopping-cart/get-all`);
}

//删除购物车商品
export async function deleteShoppingCart(id: string) {
	return request<boolean>(`/shopping-cart/delete?id=${id}`, {
		method: 'DELETE',
	});
}