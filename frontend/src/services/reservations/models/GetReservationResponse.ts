export interface GetReservationResponse {
  ReservationId: number;
  PatientId: number;
  DoctorId: string;
  Date: Date;
  Shift: number;
}
