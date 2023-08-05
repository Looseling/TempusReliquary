import axios from "axios";
import React, { useState, useEffect } from "react";
import "../../css/globalStyles.css";

interface UploadFormProps {
  timeCapsuleId: string;
}

function AddTimeCapsuleItem({ timeCapsuleId }: UploadFormProps) {
  const [file, setFile] = useState<File | null>(null);
  const [text, setText] = useState<string>("");
  const [formData, setFormData] = useState<FormData | null>(null);
  const [uploadProgress, setUploadProgress] = useState<number>(0);
  const [uploaded, setUploaded] = useState<boolean>(false);

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

      // If successful upload, set uploaded to true
      setUploaded(true);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    if (uploaded) {
      // If document is uploaded successfully, clear the form fields
      setFile(null);
      setText("");

      // Reset the uploaded state back to false
      setUploaded(false);
    }
  }, [uploaded]);

  return (
    <div className="upload-container">
      <form onSubmit={handleSubmit} className="upload-form">
        <label htmlFor="file" className="file-input-label">
          Select a file
          <input
            type="file"
            id="file"
            className="file-input"
            onChange={handleFileChange}
          />
        </label>
        <label htmlFor="text" className="text-input-label">
          Add your text
          <input
            type="text"
            id="text"
            className="text-input"
            onChange={handleTextChange}
            value={text} // added value prop to clear input field after upload
          />
        </label>
        <button className="upload-button" type="submit">
          Upload
        </button>
      </form>
    </div>
  );
}

export default AddTimeCapsuleItem;
