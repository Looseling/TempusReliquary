import axios from "axios";
import React, { useState } from "react";

interface UploadFormProps {
  timeCapsuleId: string;
}

function AddTimeCapsuleItem({ timeCapsuleId }: UploadFormProps) {
  const [file, setFile] = useState<File | null>(null);
  const [text, setText] = useState<string>("");
  const [formData, setFormData] = useState<FormData | null>(null);
  const [uploadProgress, setUploadProgress] = useState<number>(0);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setFile(event.target.files[0]);
    }
  };

  const handleTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setText(event.target.value);
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!file || !text) {
      return;
    }
    try {
      const token = localStorage.getItem("token");
      const newFormData = new FormData();
      newFormData.append("file", file);
      newFormData.append("text", text);
      newFormData.append("timeCapsuleId", timeCapsuleId);

      const response = await axios.post(
        "https://localhost:44312/api/timecapsule/content",
        newFormData,
        { headers: { Authorization: `Bearer ${token}` } }
      );

      // Handle the response as needed
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="border border-success">
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="file">Select a file:</label>
          <input type="file" id="file" onChange={handleFileChange} />
        </div>
        <div>
          <label htmlFor="text">Text:</label>
          <input type="text" id="text" onChange={handleTextChange} />
        </div>
        <div>
          <button className="btn btn-secondary" type="submit">
            Upload
          </button>
        </div>
      </form>
    </div>
  );
}

export default AddTimeCapsuleItem;
