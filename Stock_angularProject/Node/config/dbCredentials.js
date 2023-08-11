const dbCredentials = () => {
  const encodedUser = 'cG9zdGdyZXM='; //postgres
  const encodedHost = 'bG9jYWxob3N0'; // localhost
  const enCodedDatabase = 'c3RvY2s='; //stock
  const encodedPassword = 'MTIzNA==' //1234
  const encodedPort = 'NTQzMg=='; //5432
  // ---------------arati--------------------
  // const encodedUser = 'cG9zdGdyZXM=';
  // const encodedHost = 'bG9jYWxob3N0';
  // const enCodedDatabase = 'c3RvY2s=';
  // const encodedPassword = 'cGFzc3dvcmQ=';
  // const encodedPort = 'NTQzMg==';
  return {
    encodedUser, encodedHost, enCodedDatabase, encodedPassword, encodedPort
  };
};
exports.dbCredentials = dbCredentials;




