import React, { useEffect, useState } from "react";
import axios from "../../api/axios";
import ImageItem from "./ImageItem";

const MovieCard = (props) => {
  const [loadedImage, setLoadedImage] = useState();
  useEffect(() => {
    const fetchImage = async () => {
      try {
        setLoadedImage(
          "https://localhost:7160/api/images/" + props.item.posterImageId
        );
      } catch (err) {
        console.log(err);
      }
    };
    fetchImage();
  }, [props.item]);

  return (
    <>
      <div className="card">
        <ImageItem src={loadedImage} />
        <ul class="list-group list-group-flush">
          <li class="list-group-item">Cras justo odio</li>
          <li class="list-group-item">Dapibus ac facilisis in</li>
          <li class="list-group-item">Vestibulum at eros</li>
        </ul>
      </div>
    </>
  );
};

export default MovieCard;
