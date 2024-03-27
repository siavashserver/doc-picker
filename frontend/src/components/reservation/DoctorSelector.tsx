import React, {useEffect, useState} from "react";
import {GetSpecialitiesRequest} from "@/services/specialities/models/GetSpecialitiesRequest";
import {SpecialitiesService} from "@/services/specialities/SpecialitiesService";
import {GetDoctorsRequest} from "@/services/doctors/models/GetDoctorsRequest";
import {DoctorsService} from "@/services/doctors/DoctorsService";
import {Flex, Select} from "antd";

export interface OnDoctorSelectedParams {
    specialityId: string;
    doctorId: string;
}

export interface DoctorSelectorProps {
    onDoctorSelected?: (params: OnDoctorSelectedParams) => void;
}

const returnUndefinedIfEmptyString = (value?: string): string | undefined => {
    if (undefined == value || "" == value.trim()) return undefined;
    return value;
};

const DoctorSelector: React.FC<DoctorSelectorProps> = (props) => {
    const [selectedSpeciality, setSelectedSpeciality] = useState<
        string | undefined
    >(undefined);
    const [specialityOptions, setSpecialityOptions] = useState<
        { value: string; label: string }[]
    >([]);
    const specialityFilterOption = (
        input: string,
        option?: { label: string; value: string }
    ) => true;
    const specialityOnSearch = async (value: string) =>
        await getSpecialities(value);
    const specialityOnChange = async (value: string) => {
        console.log(`Speciality selected ${value}`);
        setSelectedSpeciality(value);
    };
    const getSpecialities = async (specialityName?: string) => {
        const names: string[] =
            returnUndefinedIfEmptyString(specialityName) != undefined
                ? [specialityName]
                : [];
        const request: GetSpecialitiesRequest = {
            Page: 0,
            PageSize: 100,
            Descriptions: [],
            Names: names,
            SpecialityIds: []
        };
        const response = await SpecialitiesService.GetSpecialities(request);
        const _specialityOptions = response.Specialities.map((speciality) => {
            const specialityOption = {
                value: speciality.SpecialityId,
                label: speciality.Name
            };
            return specialityOption;
        });
        setSpecialityOptions(_specialityOptions);
    };

    const [selectedDoctor, setSelectedDoctor] = useState<string | undefined>(
        undefined
    );
    const [doctorOptions, setDoctorOptions] = useState<
        { value: string; label: string }[]
    >([]);
    const [doctorSelectorDisabled, setDoctorSelectorDisabled] =
        useState<boolean>(true);
    const doctorFilterOption = (
        input: string,
        option?: { label: string; value: string }
    ) => true;
    const doctorOnSearch = async (value: string) =>
        await getDoctors(selectedSpeciality, value);
    const doctorOnChange = (value: string) => {
        console.log(`Doctor selected ${value}`);
        setSelectedDoctor(value);
    };
    const getDoctors = async (specialityId?: string, doctorName?: string) => {
        const specialityIds: string[] =
            returnUndefinedIfEmptyString(specialityId) != undefined
                ? [specialityId]
                : [];
        const names: string[] =
            returnUndefinedIfEmptyString(doctorName) != undefined ? [doctorName] : [];
        const request: GetDoctorsRequest = {
            Page: 0,
            PageSize: 100,
            DoctorIds: [],
            Names: names,
            SpecialityIds: specialityIds
        };
        const response = await DoctorsService.GetDoctors(request);
        const _doctorOptions = response.Doctors.map((doctor) => {
            const doctorOption = {
                value: doctor.DoctorId,
                label: doctor.Name
            };
            return doctorOption;
        });
        setDoctorOptions(_doctorOptions);
    };

    useEffect(() => {
        getSpecialities(undefined);
    }, []);

    useEffect(() => {
        if (undefined == returnUndefinedIfEmptyString(selectedSpeciality)) return;

        setSelectedDoctor(undefined);
        getDoctors(selectedSpeciality, undefined);
        setDoctorSelectorDisabled(false);
    }, [selectedSpeciality]);

    useEffect(() => {
        if (
            undefined == props.onDoctorSelected ||
            undefined == returnUndefinedIfEmptyString(selectedSpeciality) ||
            undefined == returnUndefinedIfEmptyString(selectedDoctor)
        )
            return;

        props.onDoctorSelected({
            specialityId: selectedSpeciality,
            doctorId: selectedDoctor
        });
    }, [selectedDoctor]);

    return (
        <Flex gap={"middle"}>
            <Select
                showSearch
                placeholder="Select a speciality"
                optionFilterProp="children"
                onChange={specialityOnChange}
                onSearch={specialityOnSearch}
                filterOption={specialityFilterOption}
                options={specialityOptions}
                value={selectedSpeciality}
            />
            <Select
                showSearch
                placeholder="Select a doctor"
                optionFilterProp="children"
                onChange={doctorOnChange}
                onSearch={doctorOnSearch}
                filterOption={doctorFilterOption}
                options={doctorOptions}
                disabled={doctorSelectorDisabled}
                value={selectedDoctor}
            />
        </Flex>
    );
};

export {DoctorSelector};
