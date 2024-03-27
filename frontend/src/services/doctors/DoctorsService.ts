import {GetDoctorsRequest} from "@/services/doctors/models/GetDoctorsRequest";
import {GetDoctorsResponse} from "@/services/doctors/models/GetDoctorsResponse";
import {axiosInstance} from "@/utilities/AxiosInstance";

export class DoctorsService {
    private static get BaseUrl(): string {
        return `/Doctors`;
    }

    public static async GetDoctors(
        request: GetDoctorsRequest,
    ): Promise<GetDoctorsResponse> {
        const url = `${this.BaseUrl}/search`;
        const response = await axiosInstance.post<GetDoctorsResponse>(url, request);
        return response.data;
    }
}
