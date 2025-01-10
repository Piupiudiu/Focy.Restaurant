import { deleteShoppingCart, getShoppingCartList, ShoopingCartItemDto } from "@/services/shoppingCart";
import { Button, Checkbox, CheckboxRef, Dialog, Form, Input, List, Modal, TextArea, Toast } from "antd-mobile";
import { FC, PropsWithChildren, useEffect, useRef, useState } from "react";
import noImageJpg from '../assets/noImage.png';
import { CheckboxValue } from "antd-mobile/es/components/checkbox";
import SwipeAction, { Action } from "antd-mobile/es/components/swipe-action";
import { createOrder } from "@/services/order";

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
	const [modalVisible, setModalVisible] = useState(false);
	const [form] = Form.useForm();

	const onFinish = (values: any) => {
		createOrder({
			name: values.name,
			remark: values.remark,
			menuIds: checkedValue as string[],
		}).then(res => {
			if (res) {
				Toast.show({
					icon: 'success',
					content: '下单成功!',
				});
				getShoppingCartList().then(res => setShoppingCartList(res));
			}
			else {
				Toast.show({
					icon: 'fail',
					content: '下单失败!',
				})
			}
		});
		form.resetFields();
		setModalVisible(false);
		setCheckedValue([]);
	}

	const onOrderBtnClick = () => {
		if (checkedValue.length === 0) {
			Toast.show({
				icon: 'fail',
				content: '请选择要下单的菜单!',
			})
			return;
		}
		else {
			setModalVisible(true);
		}
	}

	useEffect(() => {
		getShoppingCartList().then(res => setShoppingCartList(res));
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
								onAction={() => deleteShoppingCart(item.id).then(res => {
									getShoppingCartList().then(res => setShoppingCartList(res))
								})}
							>
								<ListItemWithCheckbox key={item.id} item={item} />
							</SwipeAction>
						))
					}
				</List>
			</Checkbox.Group>
			<div style={{ backgroundColor: '#d4e3f2', display: 'flex', justifyContent: 'space-between', marginBottom: '0px', paddingTop: '8px', paddingBottom: '8px', width: '100%', position: 'sticky', bottom: '0px' }}>
				<Checkbox style={{ marginLeft: '12px' }} onChange={checked => {
					if (checked) {
						setCheckedValue(shoppingCartList.map(item => {
							return item.menuId
						}))
					} else {
						setCheckedValue([]);
					}
				}}>全选</Checkbox>
				<Button style={{ borderRadius: '30px', marginRight: '16px' }} color='primary' size="small" fill='solid' onClick={onOrderBtnClick}>
					下单
				</Button>
			</div>
			<Modal
				visible={modalVisible}
				closeOnMaskClick={true}
				content={(
					<Form
						form={form}
						mode="card"
						layout='horizontal'
						onFinish={onFinish}
						footer={
							<Button block type='submit' color='primary' size='large'>
								提交
							</Button>
						}
					>
						<Form.Item
							name='name'
							label='订单名称'
							rules={[{ required: true, message: '订单名称不能为空' }]}
							style={{ fontSize: '15px'}}
						>
							<Input style={{ fontSize: '15px'}} onChange={console.log} placeholder='订单名称' />
						</Form.Item>
						<Form.Item style={{ fontSize: '15px'}} name='remark' label='备注' help='请输入少于三十个字的备注'>
							<TextArea
								placeholder='请输入备注'
								maxLength={30}
								rows={2}
								showCount
								style={{ fontSize: '15px'}}
							/>
						</Form.Item>
					</Form>
				)}
				onClose={() => {
					setModalVisible(false)
				}}
			/>
		</>
	);
}