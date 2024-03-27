import {CreateReservationRequest} from "@/services/reservations/models/CreateReservationRequest";
import {CreateReservationResponse} from "@/services/reservations/models/CreateReservationResponse";
import {GetReservationRequest} from "@/services/reservations/models/GetReservationRequest";
import {GetReservationResponse} from "@/services/reservations/models/GetReservationResponse";
import {GetReservationsRequest} from "@/services/reservations/models/GetReservationsRequest";
import {GetReservationsResponse} from "@/services/reservations/models/GetReservationsResponse";
import {UpdateReservationRequest} from "@/services/reservations/models/UpdateReservationRequest";
import {UpdateReservationResponse} from "@/services/reservations/models/UpdateReservationResponse";
import {DeleteReservationRequest} from "@/services/reservations/models/DeleteReservationRequest";
import {DeleteReservationResponse} from "@/services/reservations/models/DeleteReservationResponse";
import {axiosInstance} from "@/utilities/AxiosInstance";

export class ReservationsService {
    private static get BaseUrl(): string {
        return `/Reservations`;
    }

    public static async CreateReservation(
        request: CreateReservationRequest,
    ): Promise<CreateReservationResponse> {
        const url = `${this.BaseUrl}`;
        const response = await axiosInstance.post<CreateReservationResponse>(
            url,
            request,
        );
        return response.data;
    }

    public static async GetReservation(
        request: GetReservationRequest,
    ): Promise<GetReservationResponse> {
        const url = `${this.BaseUrl}/${request.ReservationId}`;
        const response = await axiosInstance.get<GetReservationResponse>(url);
        return response.data;
    }

    public static async GetReservations(
        request: GetReservationsRequest,
    ): Promise<GetReservationsResponse> {
        const url = `${this.BaseUrl}/search`;
        const response = await axiosInstance.post<GetReservationsResponse>(
            url,
            request,
        );
        return response.data;
    }

    public static async UpdateReservation(
        request: UpdateReservationRequest,
    ): Promise<UpdateReservationResponse> {
        const url = `${this.BaseUrl}/${request.ReservationId}`;
        const response = await axiosInstance.patch<UpdateReservationResponse>(
            url,
            request,
        );
        return response.data;
    }

    public static async DeleteReservation(
        request: DeleteReservationRequest,
    ): Promise<DeleteReservationResponse> {
        const url = `${this.BaseUrl}/${request.ReservationId}`;
        const response = await axiosInstance.delete<DeleteReservationResponse>(url);
        return response.data;
    }
}
