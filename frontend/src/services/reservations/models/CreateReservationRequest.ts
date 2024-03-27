export interface CreateReservationRequest {
    PatientId: number;
    DoctorId: string;
    Date: Date;
    Shift: number;
}
