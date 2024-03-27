export interface GetDoctorsRequest {
    Page: number;
    PageSize: number;
    DoctorIds: string[];
    Names: string[];
    SpecialityIds: string[];
}
