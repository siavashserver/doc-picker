import axios, { CreateAxiosDefaults } from "axios";
import { AppConfig } from "@/utilities/AppConfig";

const axiosDefaults: CreateAxiosDefaults = {
  baseURL: `${AppConfig.WebAPIBaseUrl}/api`,
};

const axiosInstance = axios.create(axiosDefaults);

export { axiosInstance };
