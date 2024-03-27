"use client";

import {Card, Flex, Space} from "antd";
import React from "react";

const SignIn: React.FC = () => {
    return (
        <Space direction="vertical" size={16}>
            <Card style={{width: "32rem"}}>
                <Flex gap="small" wrap="wrap" justify={"center"}>
                    You have been signed-out.
                </Flex>
            </Card>
        </Space>
    );
};

export default SignIn;
