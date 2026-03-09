<script setup lang="ts">
import { reactive, ref } from "vue";
import { useDataService } from "./composables/dataService";
import Validate from "./components/Validate.vue";

const errors = ref({});
const dataService = useDataService();
const loading = ref(false);
const employee = ref("Shawn Wildermuth");
const employeeId = ref(1);
const message = ref("");
const cases = reactive([
  {
    id: 1,
    fileNumber: "12345",
    client: {
      name: "Jones, M.",
    },
  },
  {
    id: 2,
    fileNumber: "1235",
    client: {
      name: "Smith, J.",
    },
  },
]);

const bill = reactive({
  caseId: 0,
  employeeId: 0,
  timeSegments: 0,
  workDate: Date(),
  rate: 125,
  workDescription: ""
});

function checkValidation() {
  return true;
}

function save() {
  if (checkValidation()) {
    bill.employeeId = employeeId.value;
    dataService.saveTimeBill(bill);
    message.value = "Saved...";
    setTimeout(() => (message.value = ""), 2000);
  }
}
</script>

<template>
  <div class="flex p-2 text-lg w-full ">
    <div class="w-4/5 md:w-1/2 mx-auto border p-2 rounded border-white/25">
      <h3 class="text-2xl font-bold">Add Timesheet</h3>
      <div class="loading loading-ring" v-if="loading"></div>
      <div class="toast" v-if="message">
        <div class="alert alert-info">
          {{ message }}
        </div>
      </div>
      <div>
        Employee: <i>{{ employee }}</i>
      </div>

      <form novalidate @submit.prevent="save()">
        <div class="flex flex-col p-2">
          <label class="my-1 font-bold">Case:</label>
          <select
            class="select select-primary w-full appearance-none text-lg"
            v-model="bill.caseId"
            @change="checkValidation"
          >
            <option disabled selected value="0">Pick a Case...</option>
            <option v-for="caseItem in cases" :value="caseItem.id">
              {{ caseItem.client.name }}: {{ caseItem.fileNumber }}
            </option>
          </select>
          <label class="my-1 font-bold">Date:</label>
          <input
            v-model="bill.workDate"
            type="date"
            placeholder="e.g. 01-02-2026"
            class="input w-full input-primary text-lg"
            @change="checkValidation"
          />
          <label class="my-1 font-bold"
            >Time Segments (6 minute segments):</label
          >
          <input
            class="input w-full input-primary text-lg"
            placeholder="e.g. 12"
            v-model="bill.timeSegments"
            @change="checkValidation"
          />
          <label class="my-1 font-bold">Rate (in USD):</label>
          <input
            class="input w-full input-primary text-lg"
            v-model="bill.rate"
            placeholder="e.g. 125.00"
            @change="checkValidation"
          />
          <label class="my-1 font-bold">Work Performed:</label>
          <textarea
            class="textarea textarea-primary w-full text-lg"
            rows="3"
            v-model="bill.workDescription"
            placeholder="e.g. Did some work..."
            @change="checkValidation"
          ></textarea>
          <div class="flex justify-end gap-2 py-2">
            <a href="/" class="btn btn-secondary">Cancel</a>
            <button type="submit" class="btn btn-primary">Add Time Bill</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>
