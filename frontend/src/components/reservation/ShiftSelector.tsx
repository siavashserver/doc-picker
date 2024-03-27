import React from "react";
import { Segmented } from "antd";

export interface ShiftSelectorProps {
  shifts: ShiftSelectorShift[];
  onShiftSelected?: (selectedShift: number) => void;
}

export interface ShiftSelectorShift {
  isSelectable: boolean;
}

interface SegmentedOption {
  label: string;
  value: string;
  disabled: boolean;
}

const ShiftSelector: React.FC<ShiftSelectorProps> = (props) => {
  const segementedOptions: SegmentedOption[] = props.shifts.map((shift, index) => {
    const _index = index.toString();
    let segementedOption: SegmentedOption = {
      label: _index,
      value: _index,
      disabled: !shift.isSelectable
    };
    return segementedOption;
  });
  
  const onChangeHandler = (value: string) => {
    if (undefined == props.onShiftSelected || "" == value.trim()) return;
    props.onShiftSelected(parseInt(value));
  };
  
  return (
    <>
      <Segmented
        options={segementedOptions}
        onChange={onChangeHandler}
      />
    </>
  );
};

export { ShiftSelector };
