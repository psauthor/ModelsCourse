export function useDataService() {

  const baseUrl: string = "/api/";

  async function getClients() {
    const result = await fetch(baseUrl + "clients");
    const json = await result.json();
    return JSON.parse(json);
  }

  async function getCases() {
    const result = await fetch(baseUrl + "cases");
    const json = await result.json();
    return JSON.parse(json);
  }

  async function getTimeBill(id) {
    const result = await fetch(baseUrl + "timebills/" + id);
    const json = await result.json();
    return JSON.parse(json);
  }

  async function getTimeBills() {
    const result = await fetch(baseUrl + "timebills");
    const json = await result.json();
    return JSON.parse(json);
  }

  async function saveTimeBill(timeBill) {
    const result = await fetch(baseUrl + "timebills",
      {
        method: "POST",
        body: JSON.stringify(timeBill)
      });

    const json = await result.json();

    const retVal = {} as any;

    if (result.status == 201) {
      retVal.success = true;
      retVal.data = JSON.parse(json);
    } else {
      retVal.success = false;
      retVal.status = result.status;
      retVal.errors = JSON.parse(json).errors;
    }

    return retVal;
  }

  async function saveCase(theCase) {
    const result = await fetch(baseUrl + "cases",
      {
        method: "POST",
        body: JSON.stringify(theCase)
      });

    const json = await result.json();

    const retVal = {
    } as any;

    if (result.status == 201) {
      retVal.success = true;
      retVal.data = JSON.parse(json);
    } else {
      retVal.success = false;
      retVal.status = result.status;
      retVal.errors = JSON.parse(json).errors;
    }

    return retVal;
  }

  return {
    getClients,
    getCases,
    saveCase,
    getTimeBills,
    getTimeBill,
    saveTimeBill
  };
}
