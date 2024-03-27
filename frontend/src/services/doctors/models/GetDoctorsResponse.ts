import {GetDoctorsResult} from "@/services/doctors/models/GetDoctorsResult";

export interface GetDoctorsResponse {
    HasNextPage: boolean;
    Doctors: GetDoctorsResult[];
}
