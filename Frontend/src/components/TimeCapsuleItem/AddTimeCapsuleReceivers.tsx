import axios from "axios";
import React, { useState, useEffect } from "react";
import "../../css/globalStyles.css";

interface AddTimeCapsuleReceiversProps {
  timeCapsuleId: string;
}
interface Receiver {
  email: string;
}

function AddTimeCapsuleReceivers({
  timeCapsuleId,
}: AddTimeCapsuleReceiversProps) {
  const [fetchedEmails, setFetchedEmails] = useState<Receiver[]>([]);
  const [emails, setEmails] = useState<Receiver[]>([]);
  const [emailInput, setEmailInput] = useState<string>("");
  const [uploaded, setUploaded] = useState<boolean>(false);

  const handleEmailInputChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setEmailInput(event.target.value);
  };

  const isValidEmail = (email: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  };
  const handleAddEmail = () => {
    if (emailInput.trim() !== "" && isValidEmail(emailInput.trim())) {
      setEmails([...emails, { email: emailInput.trim() }]);
      setEmailInput("");
    }
  };

  const handleRemoveEmail = (index: number) => {
    const updatedEmails = emails.filter((_, i) => i !== index);
    setEmails(updatedEmails);
  };
  const fetchReceivers = async () => {
    try {
      const response = await axios.get(
        `https://localhost:44312/api/Mail/GetReceivers?timeCapsuleId=${timeCapsuleId}`
      );
      const receivers: Receiver[] = response.data;
      setFetchedEmails(receivers);
      emails.length = 0;
    } catch (error) {
      console.error(error);
    }
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (emails.length === 0) {
      return;
    }

    try {
      const emailStrings = emails.map((receiver) => receiver.email);
      await axios.post(
        `https://localhost:44312/api/Mail/Receivers?timeCapsuleId=${timeCapsuleId}`,
        emailStrings,
        { headers: { "Content-Type": "application/json" } }
      );

      setUploaded(true);
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    fetchReceivers(); // Fetch the list of receivers when the component mounts
  }, []);

  useEffect(() => {
    if (uploaded) {
      fetchReceivers(); // Fetch the updated list of receivers after form submission
      setEmailInput(""); // Clear email input field
      setUploaded(false); // Reset uploaded state
    }
  }, [uploaded]);

  return (
    <div className="upload-container">
      <form onSubmit={handleSubmit} className="upload-form">
        <div className="email-input-container">
          <div>
            <h2>Fetched Emails:</h2>
            {fetchedEmails.map((receiver, index) => (
              <div key={index} className="email-item">
                {receiver.email}
              </div>
            ))}
          </div>
          <div>
            <h2>New Emails:</h2>
            {emails.map((receiver, index) => (
              <div key={index} className="email-item">
                {receiver.email}
                <button
                  type="button"
                  className="remove-email-button"
                  onClick={() => handleRemoveEmail(index)}
                >
                  &times;
                </button>
              </div>
            ))}
          </div>
          <div className="email-input-wrapper">
            <input
              type="email"
              id="email"
              className="email-input"
              placeholder="Add an email"
              onChange={handleEmailInputChange}
              value={emailInput}
            />
            <button
              type="button"
              className="add-email-button"
              onClick={handleAddEmail}
            >
              Add
            </button>
          </div>
        </div>
        <button className="upload-button" type="submit">
          Upload
        </button>
      </form>
    </div>
  );
}

export default AddTimeCapsuleReceivers;
