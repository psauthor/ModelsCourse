<script setup lang="ts">
import { computed } from 'vue';
import { ZodError } from 'zod';
import { isEqual } from "lodash-es";


const message = computed(() => {
  if (!props.errors || !props.field) return "";
  const path = props.field.split(".");
  const error = props.errors.issues.find(e => isEqual(e.path, path));
  if (error) {
    return error.message;
  }
})

const props = defineProps({
  errors: {
    required: true
  },
  field: {
    required: true
  }
});
</script>

<template>
  <span v-if="props.errors" class="text-error text-sm italic">{{ message }}</span>
</template>
