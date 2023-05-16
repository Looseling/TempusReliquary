import React, { ChangeEvent, useState } from "react";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import TimeCapsuleService from "../services/TimeCapsuleService";

function AddTimeCapsule() {
  const initialTimeCapsuleState = {
    id: 0,
    title: "",
    description: "",
    openingDate: "",
  };

  const [TimeCapsule, SetTimeCapsule] = useState<ITimeCapsuleModel>(
    initialTimeCapsuleState
  );
  const [submitted, setSubmitted] = useState<boolean>(false);

  const handleInputChange = (e: any) => {
    const { name, value } = e.target;
    SetTimeCapsule({ ...TimeCapsule, [name]: value });
  };

  const SaveTimeCapsule = () => {
    const data = {
      id: 0,
      title: TimeCapsule.title,
      description: TimeCapsule.description,
      openingDate: TimeCapsule.openingDate,
    };

    TimeCapsuleService.create(data)
      .then((response: any) => {
        SetTimeCapsule({
          id: response.data.id,
          title: response.data.title,
          description: response.data.description,
          openingDate: response.data.openingDate,
        });
        setSubmitted(true);
        console.log(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };

  const newTimeCapsule = () => {
    SetTimeCapsule(initialTimeCapsuleState);
    setSubmitted(false);
  };

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>You submitted successfully!</h4>
          <button className="btn btn-success" onClick={newTimeCapsule}>
            Add
          </button>
        </div>
      ) : (
        <div>
          <div className="form-group">
            <label htmlFor="title">Title</label>
            <input
              type="text"
              className="form-control"
              id="title"
              required
              value={TimeCapsule.title}
              onChange={handleInputChange}
              name="title"
            />
          </div>

          <div className="form-group">
            <label htmlFor="description">Description</label>
            <input
              type="text"
              className="form-control"
              id="description"
              required
              value={TimeCapsule.description}
              onChange={handleInputChange}
              name="description"
            />
          </div>
          <div className="form-group">
            <label htmlFor="openingDate">Opening Date</label>
            <input
              type="date"
              className="form-control"
              id="openingDate"
              required
              value={TimeCapsule.openingDate} // Set the value as a string in "YYYY-MM-DD" format
              onChange={handleInputChange}
              name="openingDate"
            />
          </div>

          <button onClick={SaveTimeCapsule} className="btn btn-success">
            Submit
          </button>
        </div>
      )}
    </div>
  );
}

export default AddTimeCapsule;
