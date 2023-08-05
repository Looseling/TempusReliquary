import React, { useEffect, useState } from "react";

interface GetTimeCapsuleItemProps {
  timeCapsuleId: string;
}
const fetchData = async (timeCapsuleId: string) => {
  const token = localStorage.getItem("token");
  const response = await fetch(
    `https://localhost:44312/api/timecapsule/content?timeCapsuleId=${timeCapsuleId}`,
    {
      headers: { Authorization: `Bearer ${token}` },
    }
  );
  const data = await response.json();
  return data;
};

function GetTimeCapsuleItem({ timeCapsuleId }: GetTimeCapsuleItemProps) {
  const [links, setLinks] = useState([]);
  const fetchLinks = async () => {
    const data = await fetchData(timeCapsuleId);
    setLinks(data);
  };

  useEffect(() => {
    fetchLinks();
  }, [timeCapsuleId]);

  return (
    <div className="border border-success">
      <h4>Items</h4>
      {links.map((link, index) => (
        <>
          <a key={index} href={link}>
            {link}
          </a>
          <br />
        </>
      ))}
    </div>
  );
}

export default GetTimeCapsuleItem;
