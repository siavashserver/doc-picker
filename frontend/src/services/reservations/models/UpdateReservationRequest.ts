export interface UpdateReservationRequest {
    ReservationId: number;
    PatientId: number;
    DoctorId: string;
    Date: Date;
    Shift: number;
}
