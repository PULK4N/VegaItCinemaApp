import { CFormCheck } from "@coreui/react";
import React from "react";
import "./NameFilter.css";

const NameFilterItem = (props) => {
  const letters = [
    "A",
    "B",
    "C",
    "D",
    "E",
    "F",
    "G",
    "H",
    "I",
    "J",
    "K",
    "L",
    "M",
    "N",
    "O",
    "P",
    "Q",
    "R",
    "S",
    "T",
    "U",
    "V",
    "W",
    "X",
    "Y",
    "Z",
  ];

  const handleCharButtonChange = (event) => {
    var charsCopy = [...props.filterCharsArray];
    if (event.target.checked) {
      charsCopy.push(event.target.id);
    } else {
      charsCopy = charsCopy.filter((char) => char != event.target.id);
    }
    console.log(charsCopy);
    props.setFilterCharsArray(charsCopy);
  };

  return (
    <>
      <div id="name-filter">
        <label htmlFor="nameFilter">Filter: ‎‎</label>
        <input
          type="text"
          id="nameFilter"
          autoComplete="off"
          onChange={(e) => props.setFilterName(e.target.value)}
          required
        />
        <ul>
          {letters.map((item) => (
            <CFormCheck
              inline
              id={item}
              label={item}
              key={item}
              onChange={handleCharButtonChange}
            />
          ))}
        </ul>
      </div>
    </>
  );
};

export default NameFilterItem;
