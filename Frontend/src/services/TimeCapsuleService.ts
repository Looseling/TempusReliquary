import { apiClient } from "../utilities/http-common";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import axios from "axios";

export const getAll = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await apiClient.get("/timecapsules", {
      headers: { Authorization: `Bearer ${token}` },
    });
    return response;
  } catch (error) {
    console.error("Error getting time capsules:", error);
    throw error;
  }
};

const getById = async (id: any) => {
  try {
    const response = await apiClient.get<ITimeCapsuleModel>(
      `/timecapsules/${id}`
    );
    return response;
  } catch (error) {
    // Handle error
    console.error(`Error getting time capsule with ID ${id}:`, error);
    throw error;
  }
};

export const create = async (data: ITimeCapsuleModel) => {
  try {
    const token = localStorage.getItem("token");
    const response = await apiClient.post("/timecapsules", data, {
      headers: { Authorization: `Bearer ${token}` },
    });
    return response.data;
  } catch (error) {
    console.error("Error creating time capsule:", error);
    throw error;
  }
};

const update = async (id: any, data: ITimeCapsuleModel) => {
  try {
    const response = await apiClient.put<ITimeCapsuleModel>(
      `/timecapsules/${id}`,
      data
    );
    return response.data;
  } catch (error) {
    // Handle error
    console.error(`Error updating time capsule with ID ${id}:`, error);
    throw error;
  }
};

const remove = async (id: any) => {
  try {
    const response = await apiClient.delete<ITimeCapsuleModel>(
      `/timecapsules/${id}`
    );
    return response.data;
  } catch (error) {
    // Handle error
    console.error(`Error deleting time capsule with ID ${id}:`, error);
    throw error;
  }
};

const TimeCapsuleService = {
  getAll,
  getById,
  create,
  update,
  remove,
};

export default TimeCapsuleService;
