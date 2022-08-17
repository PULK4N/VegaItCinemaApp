import React, { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";
import { Link } from "react-router-dom";
import axios from "../../api/axios";
import NameFilterItem from "../../shared/components/Filter/NameFilterItem";
import useAuth from "../../shared/hooks/auth-hook";
import MovieAdminTableItem from "../components/MovieAdminTableItem";
import GenreFilterItem from "../../shared/components/Filter/GenreFilterItem";
import { CFormCheck } from "@coreui/react";
import MovieCard from "./../components/MovieCard";

const MOVIES_URL = "/api/movies";

const AllMovies = () => {
  const [movies, setMovies] = useState([]);
  const { auth } = useAuth();

  const [genreIds, setGenreIds] = useState([]);
  const [movieDay, setMovieDay] = useState([]);

  const [sortAlph, setSortAlph] = useState(false);
  const [sortChr, setSortChr] = useState(false);

  const [pageCount, setPageCount] = useState(1);
  const [pageNumber, setPageNumber] = useState(1);
  const itemsPerPage = 6;

  const toQuery = (obj) => {
    var str = [];
    for (var p in obj) {
      if (Array.isArray(obj[p])) {
        if (obj[p].length != 0)
          for (let i = 0; i < obj[p].length; i++) {
            console.log(
              encodeURIComponent(p) + "=" + encodeURIComponent(obj[p][i])
            );
            str.push(
              encodeURIComponent(p) + "=" + encodeURIComponent(obj[p][i])
            );
          }
      } else if (obj.hasOwnProperty(p)) {
        if (obj[p].length != 0) {
          console.log(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
          str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
        }
      }
    }
    return str.join("&");
  };

  const getMovies = async () => {
    try {
      console.log(genreIds);
      var url = MOVIES_URL;
      url += `?PageNumber=${pageNumber}&PageSize=${itemsPerPage}?SortAlphabetically=${sortAlph}&SortMovieScreeningsChronologically=${sortChr}`;
      if (genreIds.length != 0) {
        url += `&${toQuery({
          genreIds: Array.from(genreIds),
          movieDay: movieDay,
        })}`;
      } else {
        url += `&${toQuery({ movieDay: movieDay })}`;
      }
      console.log(url);
      const response = await axios.get(url, {
        headers: {
          Authorization: `Bearer ${auth?.accessToken}`,
        },
      });
      const resMovies = response?.data?.data;
      setMovies(resMovies);
      setPageCount(response?.data?.totalPages);
      console.log(response);
    } catch (err) {
      console.log(err);
    }
  };

  const handlePageClick = (event) => {
    const newOffset = event.selected + 1;
    setPageNumber(newOffset);
  };

  useEffect(() => {
    getMovies();
  }, [genreIds, movieDay, sortAlph, sortChr]);

  useEffect(() => {
    const endOffset = pageNumber + itemsPerPage;
    getMovies();
  }, [pageNumber]);

  {
    /* ].[Name], [t6].[OriginalName], [t6].[PosterImageId], [t6].[Rating], [t14].[Id], [t14].[MovieId], [t14].[NumOfColumns], [t14].[NumOfRows], [t14].[StartTime], [t14].[TicketPrice], [t15].[GenresId], [t15].[MoviesId], [t15].[Id], [t15].[Name]\r\nFROM (\r\n    SELECT [m].[Id], [m].[DurationMinutes], [m].[Name], [m].[OriginalName], [m].[PosterImageId], [m].[Rating]\r\n    FROM [Movie] AS [m]\r\n    WHERE ((\r\n        SELECT COUNT(*)\r\n        FROM [MovieScreening] AS [m0]\r\n        WHERE [m].[Id] = [m0].[MovieId]) > 0) AND EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g]\r\n        INNER JOIN [Genre] AS [g0] ON [g].[GenresId] = [g0].[Id]\r\n        WHERE ([m].[Id] = [g].[MoviesId]) AND ([g0].[Id] = @__entity_equality_genre_1_Id))\r\n    UNION\r\n    SELECT [m1].[Id], [m1].[DurationMinutes], [m1].[Name], [m1].[OriginalName], [m1].[PosterImageId], [m1].[Rating]\r\n    FROM [Movie] AS [m1]\r\n    WHERE (((\r\n        SELECT COUNT(*)\r\n        FROM [MovieScreening] AS [m2]\r\n        WHERE [m1].[Id] = [m2].[MovieId]) > 0) AND EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g1]\r\n        INNER JOIN [Genre] AS [g2] ON [g1].[GenresId] = [g2].[Id]\r\n        WHERE ([m1].[Id] = [g1].[MoviesId]) AND ([g2].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g3]\r\n        INNER JOIN [Genre] AS [g4] ON [g3].[GenresId] = [g4].[Id]\r\n        WHERE ([m1].[Id] = [g3].[MoviesId]) AND ([g4].[Id] = @__entity_equality_genre_2_Id))\r\n    UNION\r\n    SELECT [t1].[Id], [t1].[DurationMinutes], [t1].[Name], [t1].[OriginalName], [t1].[PosterImageId], [t1].[Rating]\r\n    FROM (\r\n        SELECT [m3].[Id], [m3].[DurationMinutes], [m3].[Name], [m3].[OriginalName], [m3].[PosterImageId], [m3].[Rating]\r\n        FROM [Movie] AS [m3]\r\n        WHERE ((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m4]\r\n            WHERE [m3].[Id] = [m4].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g5]\r\n            INNER JOIN [Genre] AS [g6] ON [g5].[GenresId] = [g6].[Id]\r\n            WHERE ([m3].[Id] = [g5].[MoviesId]) AND ([g6].[Id] = @__entity_equality_genre_1_Id))\r\n        UNION\r\n        SELECT [m5].[Id], [m5].[DurationMinutes], [m5].[Name], [m5].[OriginalName], [m5].[PosterImageId], [m5].[Rating]\r\n        FROM [Movie] AS [m5]\r\n        WHERE (((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m6]\r\n            WHERE [m5].[Id] = [m6].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g7]\r\n            INNER JOIN [Genre] AS [g8] ON [g7].[GenresId] = [g8].[Id]\r\n            WHERE ([m5].[Id] = [g7].[MoviesId]) AND ([g8].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g9]\r\n            INNER JOIN [Genre] AS [g10] ON [g9].[GenresId] = [g10].[Id]\r\n            WHERE ([m5].[Id] = [g9].[MoviesId]) AND ([g10].[Id] = @__entity_equality_genre_2_Id))\r\n    ) AS [t1]\r\n    WHERE EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g11]\r\n        INNER JOIN [Genre] AS [g12] ON [g11].[GenresId] = [g12].[Id]\r\n        WHERE ([t1].[Id] = [g11].[MoviesId]) AND ([g12].[Id] = @__entity_equality_genre_3_Id))\r\n    UNION\r\n    SELECT [t3].[Id], [t3].[DurationMinutes], [t3].[Name], [t3].[OriginalName], [t3].[PosterImageId], [t3].[Rating]\r\n    FROM (\r\n        SELECT [m7].[Id], [m7].[DurationMinutes], [m7].[Name], [m7].[OriginalName], [m7].[PosterImageId], [m7].[Rating]\r\n        FROM [Movie] AS [m7]\r\n        WHERE ((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m8]\r\n            WHERE [m7].[Id] = [m8].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g13]\r\n            INNER JOIN [Genre] AS [g14] ON [g13].[GenresId] = [g14].[Id]\r\n            WHERE ([m7].[Id] = [g13].[MoviesId]) AND ([g14].[Id] = @__entity_equality_genre_1_Id))\r\n        UNION\r\n        SELECT [m9].[Id], [m9].[DurationMinutes], [m9].[Name], [m9].[OriginalName], [m9].[PosterImageId], [m9].[Rating]\r\n        FROM [Movie] AS [m9]\r\n        WHERE (((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m10]\r\n            WHERE [m9].[Id] = [m10].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g15]\r\n            INNER JOIN [Genre] AS [g16] ON [g15].[GenresId] = [g16].[Id]\r\n            WHERE ([m9].[Id] = [g15].[MoviesId]) AND ([g16].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g17]\r\n            INNER JOIN [Genre] AS [g18] ON [g17].[GenresId] = [g18].[Id]\r\n            WHERE ([m9].[Id] = [g17].[MoviesId]) AND ([g18].[Id] = @__entity_equality_genre_2_Id))\r\n        UNION\r\n        SELECT [t5].[Id], [t5].[DurationMinutes], [t5].[Name], [t5].[OriginalName], [t5].[PosterImageId], [t5].[Rating]\r\n        FROM (\r\n            SELECT [m11].[Id], [m11].[DurationMinutes], [m11].[Name], [m11].[OriginalName], [m11].[PosterImageId], [m11].[Rating]\r\n            FROM [Movie] AS [m11]\r\n            WHERE ((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m12]\r\n                WHERE [m11].[Id] = [m12].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g19]\r\n                INNER JOIN [Genre] AS [g20] ON [g19].[GenresId] = [g20].[Id]\r\n                WHERE ([m11].[Id] = [g19].[MoviesId]) AND ([g20].[Id] = @__entity_equality_genre_1_Id))\r\n            UNION\r\n            SELECT [m13].[Id], [m13].[DurationMinutes], [m13].[Name], [m13].[OriginalName], [m13].[PosterImageId], [m13].[Rating]\r\n            FROM [Movie] AS [m13]\r\n            WHERE (((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m14]\r\n                WHERE [m13].[Id] = [m14].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g21]\r\n                INNER JOIN [Genre] AS [g22] ON [g21].[GenresId] = [g22].[Id]\r\n                WHERE ([m13].[Id] = [g21].[MoviesId]) AND ([g22].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g23]\r\n                INNER JOIN [Genre] AS [g24] ON [g23].[GenresId] = [g24].[Id]\r\n                WHERE ([m13].[Id] = [g23].[MoviesId]) AND ([g24].[Id] = @__entity_equality_genre_2_Id))\r\n        ) AS [t5]\r\n        WHERE EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g25]\r\n            INNER JOIN [Genre] AS [g26] ON [g25].[GenresId] = [g26].[Id]\r\n            WHERE ([t5].[Id] = [g25].[MoviesId]) AND ([g26].[Id] = @__entity_equality_genre_3_Id))\r\n    ) AS [t3]\r\n    WHERE EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g27]\r\n        INNER JOIN [Genre] AS [g28] ON [g27].[GenresId] = [g28].[Id]\r\n        WHERE ([t3].[Id] = [g27].[MoviesId]) AND ([g28].[Id] = @__entity_equality_genre_4_Id))\r\n    UNION\r\n    SELECT [t7].[Id], [t7].[DurationMinutes], [t7].[Name], [t7].[OriginalName], [t7].[PosterImageId], [t7].[Rating]\r\n    FROM (\r\n        SELECT [m15].[Id], [m15].[DurationMinutes], [m15].[Name], [m15].[OriginalName], [m15].[PosterImageId], [m15].[Rating]\r\n        FROM [Movie] AS [m15]\r\n        WHERE ((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m16]\r\n            WHERE [m15].[Id] = [m16].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g29]\r\n            INNER JOIN [Genre] AS [g30] ON [g29].[GenresId] = [g30].[Id]\r\n            WHERE ([m15].[Id] = [g29].[MoviesId]) AND ([g30].[Id] = @__entity_equality_genre_1_Id))\r\n        UNION\r\n        SELECT [m17].[Id], [m17].[DurationMinutes], [m17].[Name], [m17].[OriginalName], [m17].[PosterImageId], [m17].[Rating]\r\n        FROM [Movie] AS [m17]\r\n        WHERE (((\r\n            SELECT COUNT(*)\r\n            FROM [MovieScreening] AS [m18]\r\n            WHERE [m17].[Id] = [m18].[MovieId]) > 0) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g31]\r\n            INNER JOIN [Genre] AS [g32] ON [g31].[GenresId] = [g32].[Id]\r\n            WHERE ([m17].[Id] = [g31].[MoviesId]) AND ([g32].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g33]\r\n            INNER JOIN [Genre] AS [g34] ON [g33].[GenresId] = [g34].[Id]\r\n            WHERE ([m17].[Id] = [g33].[MoviesId]) AND ([g34].[Id] = @__entity_equality_genre_2_Id))\r\n        UNION\r\n        SELECT [t10].[Id], [t10].[DurationMinutes], [t10].[Name], [t10].[OriginalName], [t10].[PosterImageId], [t10].[Rating]\r\n        FROM (\r\n            SELECT [m19].[Id], [m19].[DurationMinutes], [m19].[Name], [m19].[OriginalName], [m19].[PosterImageId], [m19].[Rating]\r\n            FROM [Movie] AS [m19]\r\n            WHERE ((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m20]\r\n                WHERE [m19].[Id] = [m20].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g35]\r\n                INNER JOIN [Genre] AS [g36] ON [g35].[GenresId] = [g36].[Id]\r\n                WHERE ([m19].[Id] = [g35].[MoviesId]) AND ([g36].[Id] = @__entity_equality_genre_1_Id))\r\n            UNION\r\n            SELECT [m21].[Id], [m21].[DurationMinutes], [m21].[Name], [m21].[OriginalName], [m21].[PosterImageId], [m21].[Rating]\r\n            FROM [Movie] AS [m21]\r\n            WHERE (((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m22]\r\n                WHERE [m21].[Id] = [m22].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g37]\r\n                INNER JOIN [Genre] AS [g38] ON [g37].[GenresId] = [g38].[Id]\r\n                WHERE ([m21].[Id] = [g37].[MoviesId]) AND ([g38].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g39]\r\n                INNER JOIN [Genre] AS [g40] ON [g39].[GenresId] = [g40].[Id]\r\n                WHERE ([m21].[Id] = [g39].[MoviesId]) AND ([g40].[Id] = @__entity_equality_genre_2_Id))\r\n        ) AS [t10]\r\n        WHERE EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g41]\r\n            INNER JOIN [Genre] AS [g42] ON [g41].[GenresId] = [g42].[Id]\r\n            WHERE ([t10].[Id] = [g41].[MoviesId]) AND ([g42].[Id] = @__entity_equality_genre_3_Id))\r\n        UNION\r\n        SELECT [t11].[Id], [t11].[DurationMinutes], [t11].[Name], [t11].[OriginalName], [t11].[PosterImageId], [t11].[Rating]\r\n        FROM (\r\n            SELECT [m23].[Id], [m23].[DurationMinutes], [m23].[Name], [m23].[OriginalName], [m23].[PosterImageId], [m23].[Rating]\r\n            FROM [Movie] AS [m23]\r\n            WHERE ((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m24]\r\n                WHERE [m23].[Id] = [m24].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g43]\r\n                INNER JOIN [Genre] AS [g44] ON [g43].[GenresId] = [g44].[Id]\r\n                WHERE ([m23].[Id] = [g43].[MoviesId]) AND ([g44].[Id] = @__entity_equality_genre_1_Id))\r\n            UNION\r\n            SELECT [m25].[Id], [m25].[DurationMinutes], [m25].[Name], [m25].[OriginalName], [m25].[PosterImageId], [m25].[Rating]\r\n            FROM [Movie] AS [m25]\r\n            WHERE (((\r\n                SELECT COUNT(*)\r\n                FROM [MovieScreening] AS [m26]\r\n                WHERE [m25].[Id] = [m26].[MovieId]) > 0) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g45]\r\n                INNER JOIN [Genre] AS [g46] ON [g45].[GenresId] = [g46].[Id]\r\n                WHERE ([m25].[Id] = [g45].[MoviesId]) AND ([g46].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g47]\r\n                INNER JOIN [Genre] AS [g48] ON [g47].[GenresId] = [g48].[Id]\r\n                WHERE ([m25].[Id] = [g47].[MoviesId]) AND ([g48].[Id] = @__entity_equality_genre_2_Id))\r\n            UNION\r\n            SELECT [t13].[Id], [t13].[DurationMinutes], [t13].[Name], [t13].[OriginalName], [t13].[PosterImageId], [t13].[Rating]\r\n            FROM (\r\n                SELECT [m27].[Id], [m27].[DurationMinutes], [m27].[Name], [m27].[OriginalName], [m27].[PosterImageId], [m27].[Rating]\r\n                FROM [Movie] AS [m27]\r\n                WHERE ((\r\n                    SELECT COUNT(*)\r\n                    FROM [MovieScreening] AS [m28]\r\n                    WHERE [m27].[Id] = [m28].[MovieId]) > 0) AND EXISTS (\r\n                    SELECT 1\r\n                    FROM [GenreMovie] AS [g49]\r\n                    INNER JOIN [Genre] AS [g50] ON [g49].[GenresId] = [g50].[Id]\r\n                    WHERE ([m27].[Id] = [g49].[MoviesId]) AND ([g50].[Id] = @__entity_equality_genre_1_Id))\r\n                UNION\r\n                SELECT [m29].[Id], [m29].[DurationMinutes], [m29].[Name], [m29].[OriginalName], [m29].[PosterImageId], [m29].[Rating]\r\n                FROM [Movie] AS [m29]\r\n                WHERE (((\r\n                    SELECT COUNT(*)\r\n                    FROM [MovieScreening] AS [m30]\r\n                    WHERE [m29].[Id] = [m30].[MovieId]) > 0) AND EXISTS (\r\n                    SELECT 1\r\n                    FROM [GenreMovie] AS [g51]\r\n                    INNER JOIN [Genre] AS [g52] ON [g51].[GenresId] = [g52].[Id]\r\n                    WHERE ([m29].[Id] = [g51].[MoviesId]) AND ([g52].[Id] = @__entity_equality_genre_1_Id))) AND EXISTS (\r\n                    SELECT 1\r\n                    FROM [GenreMovie] AS [g53]\r\n                    INNER JOIN [Genre] AS [g54] ON [g53].[GenresId] = [g54].[Id]\r\n                    WHERE ([m29].[Id] = [g53].[MoviesId]) AND ([g54].[Id] = @__entity_equality_genre_2_Id))\r\n            ) AS [t13]\r\n            WHERE EXISTS (\r\n                SELECT 1\r\n                FROM [GenreMovie] AS [g55]\r\n                INNER JOIN [Genre] AS [g56] ON [g55].[GenresId] = [g56].[Id]\r\n                WHERE ([t13].[Id] = [g55].[MoviesId]) AND ([g56].[Id] = @__entity_equality_genre_3_Id))\r\n        ) AS [t11]\r\n        WHERE EXISTS (\r\n            SELECT 1\r\n            FROM [GenreMovie] AS [g57]\r\n            INNER JOIN [Genre] AS [g58] ON [g57].[GenresId] = [g58].[Id]\r\n            WHERE ([t11].[Id] = [g57].[MoviesId]) AND ([g58].[Id] = @__entity_equality_genre_4_Id))\r\n    ) AS [t7]\r\n    WHERE EXISTS (\r\n        SELECT 1\r\n        FROM [GenreMovie] AS [g59]\r\n        INNER JOIN [Genre] AS [g60] ON [g59].[GenresId] = [g60].[Id]\r\n        WHERE ([t7].[Id] = [g59].[MoviesId]) AND ([g60].[Id] = @__entity_equality_genre_5_Id))\r\n) AS [t6]\r\nLEFT JOIN (\r\n    SELECT [m31].[Id], [m31].[MovieId], [m31].[NumOfColumns], [m31].[NumOfRows], [m31].[StartTime], [m31].[TicketPrice]\r\n    FROM [MovieScreening] AS [m31]\r\n    WHERE CONVERT(date, [m31].[StartTime]) = @__date_Date_0\r\n) AS [t14] ON [t6].[Id] = [t14].[MovieId]\r\nLEFT JOIN (\r\n    SELECT [g61].[GenresId], [g61].[MoviesId], [g62].[Id], [g62].[Name]\r\n    FROM [GenreMovie] AS [g61]\r\n    INNER JOIN [Genre] AS [g62] ON [g61].[GenresId] = [g62].[Id]\r\n) AS [t15] ON [t6].[Id] = [t15].[MoviesId]\r\nORDER BY [t6].[Id], [t14].[Id], [t15].[GenresId], [t15].[MoviesId]" */
  }
  return (
    <>
      <div className="container">
        <div>
          <input
            type="date"
            id="nameFilter"
            autoComplete="off"
            onChange={(e) => setMovieDay(e.target.value)}
            required
          />
          <text>‎</text> ‎
          <CFormCheck
            inline
            id="alphabetical"
            label="Sort chronologically"
            onChange={() => setSortAlph(!sortAlph)}
          />
          <CFormCheck
            inline
            id="chronological"
            label="Sort chronologically"
            onChange={() => setSortChr(!sortChr)}
          />
          <GenreFilterItem
            filterGenresArray={genreIds}
            setFilterGenresArray={setGenreIds}
          ></GenreFilterItem>
        </div>
        <div className="page-items">
          <div>
            <div class="row container">
              {/* <div class=""> */}
              {movies.map((item) => (
                <MovieCard item={item}></MovieCard>
              ))}
              {/* </div> */}
            </div>
          </div>
          <ReactPaginate
            breakLabel="..."
            nextLabel="next >"
            onPageChange={handlePageClick}
            pageRangeDisplayed={5}
            pageCount={pageCount}
            previousLabel="< previous"
            renderOnZeroPageCount={null}
            itemClass="page-item"
            linkClass="page-link"
          />
        </div>
      </div>
    </>
  );
};

export default AllMovies;
