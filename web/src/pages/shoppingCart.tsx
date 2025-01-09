import { DeleteShoppingCart, GetShoppingCartList, ShoopingCartItemDto } from "@/services/shoppingCart";
import { Checkbox, CheckboxRef, List } from "antd-mobile";
import { FC, PropsWithChildren, useEffect, useRef, useState } from "react";
import noImageJpg from '../assets/noImage.png';
import { CheckboxValue } from "antd-mobile/es/components/checkbox";
import SwipeAction, { Action } from "antd-mobile/es/components/swipe-action";

const rightActions: Action[] = [
	{
		key: 'delete',
		text: '删除',
		color: 'danger'
	},
]

const ListItemWithCheckbox: FC<
	PropsWithChildren<{
		item: ShoopingCartItemDto
	}>
> = props => {
	const checkboxRef = useRef<CheckboxRef>(null)
	return (
		<List.Item
			prefix={
				<div onClick={e => e.stopPropagation()}>
					<Checkbox value={props.item.menuId} ref={checkboxRef} />
				</div>
			}
			onClick={() => {
				checkboxRef.current?.toggle()
			}}
			arrow={false}
		>
			<div style={{ display: 'flex', flexDirection: 'row' }}>
				<img src={props.item.imgUri ? `https://localhost:44366/${props.item.imgUri}` : noImageJpg} style={{ width: "25%", aspectRatio: '1/1' }} />
				<div style={{ width: '75%', paddingLeft: '10px' }}>
					<div>{props.item.name}</div>
					<div>数量: 1</div>
				</div>
			</div>
		</List.Item>
	)
}

export default function ShoppingCartPage() {
	const [shoppingCartList, setShoppingCartList] = useState<ShoopingCartItemDto[]>([]);
	const [checkedValue, setCheckedValue] = useState<CheckboxValue[]>([]);

	useEffect(() => {
		GetShoppingCartList().then(res => setShoppingCartList(res));
	}, []);

	return (
		<>
			<Checkbox.Group value={checkedValue} onChange={value => setCheckedValue(value)}>
				<List header='购物车'>
					{
						shoppingCartList.map(item => (
							<SwipeAction
								key={item.id}
								rightActions={rightActions}
								onAction={() => DeleteShoppingCart(item.id).then(res => {
									GetShoppingCartList().then(res => setShoppingCartList(res))
								})}
							>
								<ListItemWithCheckbox key={item.id} item={item} />
							</SwipeAction>
						))
					}
				</List>
			</Checkbox.Group>
		</>
	);
}