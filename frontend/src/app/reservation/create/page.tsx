"use client";

import React, {useState} from "react";
import {Button, Card, message, Space, StepProps, Steps, theme} from "antd";
import {DoctorSelector, OnDoctorSelectedParams,} from "@/components/reservation/DoctorSelector";
import {DateSelector} from "@/components/reservation/DateSelector";

const CreateReservation: React.FC = () => {
    const onDoctorSelectedHandler = (params: OnDoctorSelectedParams) => {
        message.info(`Doctor selected: ${params.specialityId}, ${params.doctorId}`);
    };

    const steps = [
        {
            title: "Doctor",
            content: <DoctorSelector onDoctorSelected={onDoctorSelectedHandler}/>,
        },
        {
            title: "Date",
            content: <DateSelector/>,
        },
        {
            title: "Reserve",
            content: "",
        },
    ];

    const {token} = theme.useToken();
    const [current, setCurrent] = useState(0);

    const next = () => {
        setCurrent(current + 1);
    };

    const prev = () => {
        setCurrent(current - 1);
    };

    const items: StepProps[] = steps.map((item) => {
        const stepProps: StepProps = {title: item.title};
        return stepProps;
    });

    const contentStyle: React.CSSProperties = {
        lineHeight: "16rem",
        textAlign: "center",
        color: token.colorTextTertiary,
        backgroundColor: token.colorFillAlter,
        borderRadius: token.borderRadiusLG,
        border: `1px dashed ${token.colorBorder}`,
        marginTop: "2rem",
        minHeight: "8rem",
        padding: "2rem",
    };

    return (
        <Space direction="vertical" size={16}>
            <Card title="Create a reservation" style={{width: "32rem"}}>
                <Steps current={current} items={items}/>
                <div style={contentStyle}>{steps[current].content}</div>

                <div style={{marginTop: "2rem"}}>
                    {current < steps.length - 1 && (
                        <Button type="primary" onClick={() => next()}>
                            Next
                        </Button>
                    )}
                    {current === steps.length - 1 && (
                        <Button
                            type="primary"
                            onClick={() => message.success("Processing complete!")}
                        >
                            Done
                        </Button>
                    )}
                    {current > 0 && (
                        <Button style={{margin: "0 8px"}} onClick={() => prev()}>
                            Previous
                        </Button>
                    )}
                </div>
            </Card>
        </Space>
    );
};

export default CreateReservation;
