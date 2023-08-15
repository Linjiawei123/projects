import store from "../store/index"
const isHasRgihts = (str) => {
    var rights = [];
    rights.push(store.state.userInfo.rights);
    return rights.includes(str) > -1;
};

export {
    isHasRgihts
};