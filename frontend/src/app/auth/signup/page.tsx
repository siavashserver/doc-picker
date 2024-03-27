"use client";

import {Button, Card, Form, FormProps, Input, Space} from "antd";
import React from "react";
import {AccountsService} from "@/services/accounts/AccountsService";

const onFinish: FormProps<FormFields>["onFinish"] = async (values) => {
    const response = await AccountsService.createAccount({
        Email: values.email,
        Password: values.password,
    });
    console.log("Success:", values, response);
};

const onFinishFailed: FormProps<FormFields>["onFinishFailed"] = (errorInfo) => {
    console.log("Failed:", errorInfo);
};

const SignIn: React.FC = () => {
    return (
        <Space direction="vertical" size={16}>
            <Card title="Sign-up" style={{width: "32rem"}}>
                <Form
                    name="basic"
                    labelCol={{span: 8}}
                    wrapperCol={{span: 16}}
                    onFinish={onFinish}
                    onFinishFailed={onFinishFailed}
                    autoComplete="off"
                >
                    <Form.Item<FormFields>
                        label="Email Address"
                        name="email"
                        rules={[
                            {
                                required: true,
                                message: "Please enter a valid email address.",
                            },
                        ]}
                    >
                        <Input/>
                    </Form.Item>

                    <Form.Item<FormFields>
                        label="Password"
                        name="password"
                        rules={[
                            {required: true, message: "Please enter a valid password."},
                        ]}
                    >
                        <Input.Password/>
                    </Form.Item>

                    <Form.Item wrapperCol={{offset: 8, span: 16}}>
                        <Button type="primary" htmlType="submit">
                            Sign-up
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </Space>
    );
};

export default SignIn;

interface FormFields {
    email: string;
    password: string;
}
