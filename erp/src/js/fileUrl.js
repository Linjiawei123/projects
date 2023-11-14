import path from "../api/path"
const fileUrl = (str) => {
    return path.baseUrl + "UploadFile" + str;
};

export {
    fileUrl
};