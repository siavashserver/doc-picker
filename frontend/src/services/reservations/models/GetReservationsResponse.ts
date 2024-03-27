import {GetReservationsResult} from "@/services/reservations/models/GetReservationsResult";

export interface GetReservationsResponse {
    HasNextPage: boolean;
    Reservations: GetReservationsResult[];
}
