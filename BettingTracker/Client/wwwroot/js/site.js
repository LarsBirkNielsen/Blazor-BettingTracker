//window.importDataFunctions = {
//    getFileContentAsBase64: async (input) => {
//        return new Promise((resolve, reject) => {
//            if (input.files.length === 0) {
//                resolve(null);
//                return;
//            }
//            const reader = new FileReader();
//            reader.onload = () => {
//                resolve(reader.result);
//            };
//            reader.onerror = () => {
//                reject(reader.error);
//            };
//            reader.readAsDataURL(input.files[0]);
//        });
//    }
//};
