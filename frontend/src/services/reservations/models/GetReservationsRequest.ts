import { ReservationDateRange } from "@/services/reservations/models/ReservationDateRange";
import { ReservationShiftRange } from "@/services/reservations/models/ReservationShiftRange";

export interface GetReservationsRequest {
  Page: number;
  PageSize: number;
  ReservationIds: number[];
  PatientIds: number[];
  DoctorIds: string[];
  Dates: ReservationDateRange;
  Shifts: ReservationShiftRange;
}
