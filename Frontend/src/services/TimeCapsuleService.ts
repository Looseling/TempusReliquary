import http from "../utilities/http-common";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import axios from "axios";

const getAll = async () => {
  try {
    const response = await http.get<Array<ITimeCapsuleModel>>("/timecapsules");
    return response;
  } catch (error) {
    // Handle error
    console.error("Error getting time capsules:", error);
    throw error;
  }
};

const getById = async (id: any) => {
  try {
    const response = await http.get<ITimeCapsuleModel>(`/timecapsules/${id}`);
    return response;
  } catch (error) {
    // Handle error
    console.error(`Error getting time capsule with ID ${id}:`, error);
    throw error;
  }
};

const create = async (data: ITimeCapsuleModel) => {
  try {
    const response = await axios.post(
      "https://localhost:44312/api/timecapsules",
      data
    );
    return response.data;
  } catch (error) {
    // Handle error
    console.error("Error creating time capsule:", error);
    throw error;
  }
};

const update = async (id: any, data: ITimeCapsuleModel) => {
  try {
    const response = await http.put<ITimeCapsuleModel>(
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
    const response = await http.delete<ITimeCapsuleModel>(
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
