import React, {useEffect, useState} from "react";
import {ShiftSelector} from "@/components/reservation/ShiftSelector";
import {ReservationsService} from "@/services/reservations/ReservationsService";
import {GetReservationsRequest} from "@/services/reservations/models/GetReservationsRequest";

export interface DateSelectorProps {
    doctorId?: string;
}

const DateSelector: React.FC<DateSelectorProps> = (props) => {
    const [reservations, setReservations] = useState();

    const getReservations = async (doctorId: string) => {
        const startDate = new Date();
        const endDate = new Date(startDate);
        endDate.setDate(startDate.getDate() + 7);

        const request: GetReservationsRequest = {
            Page: 0,
            PageSize: 100,
            ReservationIds: [],
            PatientIds: [],
            DoctorIds: [doctorId],
            Dates: {
                Start: startDate,
                End: endDate
            },
            Shifts: {
                Start: 0,
                End: 4
            }
        };
        const response = await ReservationsService.GetReservations(request);
        // TODO: Pass received reservations to the shift selector component
    };

    useEffect(() => {
        const doctorId = props.doctorId;
        if (undefined == doctorId || "" == doctorId.trim()) return;
        getReservations(doctorId);
    }, [props.doctorId]);

    return <ShiftSelector shifts={[{isSelectable: true}, {isSelectable: false}, {isSelectable: true}]}/>
};

export {DateSelector};
